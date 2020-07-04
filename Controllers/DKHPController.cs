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
        MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: DKHP
        [HttpGet]
        public async Task<ActionResult> GetDKHP()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];

            if (session != null)
            {
                var idsv = session.idsv;
                ViewBag.id = idsv;
                var iddkhp = db.dangkyhocphans
                    .Where(x => x.thoigianbd <= DateTime.Now && x.thoigiankt >= DateTime.Now)
                    .Select(x => x.iddkhp)
                    .FirstOrDefault();

                if (iddkhp != 0)
                {
                    var dslop = await db.lops.Where(x => x.iddkhp == iddkhp).ToListAsync();

                    var dslopdadk = await db.hocs.Where(x => x.idsv == idsv && x.lop.iddkhp == iddkhp).Select(x => x.lop).ToListAsync();

                    var dslopchuadk = dslop.Except(dslopdadk).ToList();

                    var dslopdadkfull = new List<lop>();
                    foreach (var item in dslopdadk)
                    {
                        if (db.hocs.Where(x => x.idlop == item.idlop).Count() >= 1)
                        {
                            dslopdadkfull.Add(item);
                        }
                    }
                    //var dslopdadkefull = dslopdadk.Except(dslopdadkfull).ToList();

                    var dslopchuadkfull = new List<lop>();
                    foreach (var item in dslopchuadk)
                    {
                        if (db.hocs.Where(x => x.idlop == item.idlop).Count() >= 1)
                        {
                            dslopchuadkfull.Add(item);
                        }
                    }
                    //var dslopchuadkefull = dslopchuadk.Except(dslopchuadkfull).ToList();

                    return View(new DBContext()
                    {
                        dslopdadk = dslopdadk.Except(dslopdadkfull).ToList(),
                        dslopchuadk = dslopchuadk.Except(dslopchuadkfull).ToList(),
                        dslopdadkfull = dslopdadkfull,
                        dslopchuadkfull = dslopchuadkfull,
                    });
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddHoc(string listid, int idsv)
        {
            var listerr = new List<string>();
            var listsuc = new List<string>();
            var list = new JavaScriptSerializer().Deserialize<List<int>>(listid);
            foreach (var idlop in list)
            {
                var sinhvien = db.sinhviens
                    .Where(x => x.idsv == idsv)
                    .FirstOrDefault();

                var lop = db.lops
                    .Where(x => x.idlop == idlop)
                    .FirstOrDefault();

                var idmontrc = db.mons
                    .Where(x => x.idmon == lop.idmon)
                    .Select(x => x.idmontruoc)
                    .FirstOrDefault();

                if (await (from h in db.hocs
                           join l in db.lops on h.idlop equals l.idlop
                           where h.idsv == idsv && l.iddkhp == lop.iddkhp && l.thu == lop.thu && !(l.tietbd > lop.tietkt || l.tietkt < lop.tietbd)
                           select h).CountAsync() != 0)
                {
                    listerr.Add("Lớp " + lop.malop + ": trùng lịch");
                }
                else if (await (from h in db.hocs
                                join l in db.lops on h.idlop equals l.idlop
                                where h.idsv == idsv && l.idmon == lop.idmon && (h.diemtb >= 5 || h.diemtb == null)
                                select h).CountAsync() != 0)
                {
                    listerr.Add("Lớp " + lop.malop + ": môn đã học");
                }
                else if ((idmontrc != null) &&
                        (await (from h in db.hocs
                                join l in db.lops on h.idlop equals l.idlop
                                where h.idsv == idsv && l.idmon == idmontrc
                                select h).CountAsync() == 0))
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

        // GET: HuyDKHP
        public async Task<ActionResult> GetHuyDKHP()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];

            if (session != null)
            {
                var idsv = session.idsv;
                ViewBag.id = idsv;
                var iddkhp = await db.dangkyhocphans
                    .Where(x => x.thoigianbd <= DateTime.Now && x.thoigiankt >= DateTime.Now)
                    .Select(x => x.iddkhp)
                    .FirstOrDefaultAsync();

                if (iddkhp != 0)
                {
                    var dslopdadk = db.hocs
                        .Where(x => x.idsv == idsv && x.lop.iddkhp == iddkhp)
                        .Select(x => x.lop);

                    return View(await dslopdadk.ToListAsync());
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteHoc(string listid, int idsv)
        {
            var listerr = new List<string>();
            var listsuc = new List<string>();
            var list = new JavaScriptSerializer().Deserialize<List<int>>(listid);

            foreach (var idlop in list)
            {
                var lop = await db.lops
                    .Where(x => x.idlop == idlop)
                    .FirstOrDefaultAsync();

                if (await (from h in db.hocs
                           join l in db.lops on h.idlop equals l.idlop
                           where h.idsv == idsv && l.mon.idmontruoc != null && l.mon.idmontruoc == lop.mon.idmon
                           select h).CountAsync() != 0)
                {
                    listerr.Add("Lớp " + lop.malop + ": là môn học trước, phải huỷ môn sau");
                }
                else
                {
                    listsuc.Add("Lớp " + lop.malop + ": huỷ đăng ký thành công");
                    var hoc = await db.hocs
                        .Where(x => x.idlop == idlop && x.idsv == idsv)
                        .FirstOrDefaultAsync();
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

        [HttpGet]
        public async Task<ActionResult> GetTKB()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                ViewBag.sv = db.sinhviens.Where(x => x.idsv == session.idsv).FirstOrDefault();
                var dkhp = db.dangkyhocphans.OrderByDescending(x => x.iddkhp);
                return View(await dkhp.ToListAsync());
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpGet]
        public async Task<JsonResult> ajaxTKB(int iddkhp, int idsv)
        {
            var tkb = await db.hocs
                .Where(x => x.idsv == idsv && x.lop.iddkhp == iddkhp)
                .Select(x => new 
                {
                    malop = x.lop.malop,
                    tenmon = x.lop.mon.tenmon,
                    mamon = x.lop.mon.mamon,
                    tengv = x.lop.giangvien.hoten,
                    tclt = x.lop.mon.tclt,
                    tcth = x.lop.mon.tcth,
                    thu = x.lop.thu,
                    tietbd = x.lop.tietbd,
                    tietkt = x.lop.tietkt,
                }).ToListAsync();

            return new JsonResult
            {
                Data = tkb,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };

        }

        [HttpGet]
        public async Task<ActionResult> GetKQHT()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                ViewBag.sv = await db.sinhviens.Where(x => x.idsv == idsv).FirstOrDefaultAsync();

                var dsdkhp = (from h in db.hocs
                              join dk in db.dangkyhocphans on h.lop.iddkhp equals dk.iddkhp
                              where h.idsv == idsv
                              select dk).Distinct();

                var dskqht = db.hocs.Where(x => x.idsv == idsv);

                return View(new DBContext()
                {
                    dsdkhp = await dsdkhp.ToListAsync(),
                    dskqht = await dskqht.ToListAsync(),
                });
            }
            else
            {
                return RedirectToRoute("login");
            }
        }
    }
}