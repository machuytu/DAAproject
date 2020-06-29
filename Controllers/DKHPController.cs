using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Data.SqlClient;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using ProjectDAA1.Models;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;

namespace ProjectDAA1.Controllers
{
    public class DKHPController : Controller
    {
        // GET: DKHP
        [HttpGet]
        public ActionResult GetDKHP()
        {
            var db = new MyDatabaseEntities9();
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            var datenow = DateTime.Now;
            var listkhoahoc = db.dangkyhocphans.Where(x => x.thoigianbd <= datenow && x.thoigiankt >= datenow).SingleOrDefault();
            if (session != null && listkhoahoc != null)
            {
                var dbcontext = new DBContext();
                var idsv = session.idsv;
                var id = listkhoahoc.iddkhp;
                var dslop = db.lops.Where(x => x.iddkhp == id).ToList();
                var dslopdadk = db.hocs.Where(x => x.idsv == idsv && x.lop.iddkhp == id).Select(x => x.lop).ToList();
                var ds = dslop.Except(dslopdadk).ToList();
                dbcontext.dslopchuahoc = ds;
                dbcontext.dslopdahoc = dslopdadk;
                return View(dbcontext);
            }
            else
            {
                return RedirectToRoute("/");
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddHoc(string listid)
        {
            var listerr = new List<string>();
            var listsuc = new List<string>();
            var list = new JavaScriptSerializer().Deserialize<List<int>>(listid);
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];

            if (session != null)
            {
                var idsv = session.idsv;
                using (var db = new MyDatabaseEntities9())
                {
                    foreach (var idlop in list)
                    {
                        var sinhvien = db.sinhviens.Where(x => x.idsv == idsv).SingleOrDefault();
                        var lop = db.lops.Where(x => x.idlop == idlop).SingleOrDefault();
                        var idmon = lop.idmon;
                        var idmontrc = db.mons.Where(x => x.idmon == lop.idmon).Select(x => x.idmontruoc).SingleOrDefault();

                        if ((from h in db.hocs
                             join l in db.lops on h.idlop equals l.idlop
                             where h.idsv == idsv && l.thu == lop.thu && !(l.tietbd > lop.tietkt || l.tietkt < lop.tietbd)
                             select h).Count() != 0)
                        {
                            listerr.Add("Lớp " + lop.malop + ": trùng lịch");
                        }
                        else if ((from h in db.hocs
                                  join l in db.lops on h.idlop equals l.idlop
                                  where h.idsv == idsv && l.idmon == lop.idmon && (h.diemtb >= 5 || h.diemtb == null)
                                  select h).Count() != 0)
                        {
                            listerr.Add("Lớp " + lop.malop + ": môn đã học");
                        }
                        else if ((idmontrc != null) &&
                                ((from h in db.hocs
                                  join l in db.lops on h.idlop equals l.idlop
                                  where h.idsv == idsv && l.idmon == idmontrc
                                  select h).Count() == 0))
                        {
                            listerr.Add("Lớp " + lop.malop + ": chưa học môn học trước");
                        }
                        else
                        {
                            listsuc.Add("Lớp " + lop.malop + ": đăng ký thành công");
                            var hoc = new hoc() { idlop = idlop, idsv = idsv, sinhvien = sinhvien, lop = lop };
                            db.hocs.Add(hoc);
                            await db.SaveChangesAsync();
                        }
                    }
                    return Json(new
                    {
                        status = true,
                        listerr = listerr,
                        listsuc = listsuc,
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        // GET: HuyDKHP
        public async Task<ActionResult> GetHuyDKHP()
        {
            var db = new MyDatabaseEntities9();

            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            var datenow = DateTime.Now;

            var listkhoahoc = db.dangkyhocphans.Where(x => x.thoigianbd <= datenow && x.thoigiankt >= datenow).SingleOrDefault();

            if (session != null && listkhoahoc != null)
            {
                var idsv = session.idsv;
                var id = listkhoahoc.iddkhp;
                var dslopdadk = db.hocs.Where(x => x.idsv == idsv && x.lop.iddkhp == id).Select(x => x.lop);
                return View(await dslopdadk.ToListAsync());
            }
            else
            {
                return RedirectToRoute("/");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteHoc(string listid)
        {
            var db = new MyDatabaseEntities9();
            var listerr = new List<string>();
            var listsuc = new List<string>();
            var list = new JavaScriptSerializer().Deserialize<List<int>>(listid);
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                foreach (var idlop in list)
                {
                    var lop = db.lops.Where(x => x.idlop == idlop).SingleOrDefault();

                    if ((from h in db.hocs
                         join l in db.lops on h.idlop equals l.idlop
                         where h.idsv == idsv && l.mon.idmontruoc != null && l.mon.idmontruoc == lop.mon.idmon
                         select h).Count() != 0)
                    {
                        listerr.Add("Lớp " + lop.malop + ": là môn học trước, phải huỷ môn sau");
                    }
                    else
                    {
                        listsuc.Add("Lớp " + lop.malop + ": huỷ đăng ký thành công");
                        var hoc = db.hocs.Where(x => x.idlop == idlop && x.idsv == idsv).SingleOrDefault();
                        db.hocs.Remove(hoc);
                        await db.SaveChangesAsync();
                    }
                }

                return Json(new
                {
                    status = true,
                    listerr = listerr,
                    listsuc = listsuc,
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetTKB()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                using (var db = new MyDatabaseEntities9())
                {
                    var dkhp = db.dangkyhocphans.OrderByDescending(x => x.iddkhp);
                    return View(await dkhp.ToListAsync());
                }
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpGet]
        public JsonResult ajaxTKB(int iddkhp, int idsv)
        {
            using (var db = new MyDatabaseEntities9())
            {
                var tkb = db.hocs.Where(x => x.idsv == idsv && x.lop.iddkhp == iddkhp)
                    .Select(x => new
                    {
                        malop = x.lop.malop,
                        thu = x.lop.thu,
                        tietbd = x.lop.tietbd,
                        tietkt = x.lop.tietkt,
                        tenmon = x.lop.mon.tenmon,
                        tengv = x.lop.giangvien.hoten,
                    }).ToList();
                return new JsonResult { Data = tkb, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetKQHT()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                using (var db = new MyDatabaseEntities9())
                {
                    var dsdkhp = (from h in db.hocs
                                  join dk in db.dangkyhocphans on h.lop.iddkhp equals dk.iddkhp
                                  where h.idsv == idsv
                                  select dk).Distinct();
                    var dsbangdiem = db.hocs.Where(x => x.idsv == idsv).Select(x => new BangDiem()
                    {
                        iddkhp = x.lop.iddkhp,
                        mamon = x.lop.mon.mamon,
                        tenmon = x.lop.mon.mamon,
                        sotc = x.lop.mon.sotc,
                        diemqt = x.diemqt,
                        diemth = x.diemth,
                        diemgk = x.diemgk,
                        diemck = x.diemck,
                        diemtb = x.diemtb,
                    });
                    var dbcontext = new DBContext();
                    dbcontext.dsdkhp = await dsdkhp.ToListAsync();
                    dbcontext.dsbd = await dsbangdiem.ToListAsync();
                    return View(dbcontext);
                }
            }
            else
            {
                return RedirectToRoute("login");
            }
        }
    }
}