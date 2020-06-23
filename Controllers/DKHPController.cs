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
                var idsv = session.idsv;
                var id = listkhoahoc.iddkhp;

                var dslop = db.lops.Where(x => x.iddkhp == id).ToList();
                var dslopdadk = db.hocs.Where(x => x.idsv == idsv && x.lop.iddkhp == id).Select(x => x.lop).ToList();
                var ds = dslop.Except(dslopdadk);

                return View(ds);
            }
            else
            {
                return RedirectToRoute("/");
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddHoc(string listid)
        {
            var list = new JavaScriptSerializer().Deserialize<List<int>>(listid);
            var listerr = new List<string>();
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                using (var db = new MyDatabaseEntities9())
                {
                    foreach (var idlop in list)
                    {
                        var err = "";

                        var sinhvien = db.sinhviens.Where(x => x.idsv == idsv).SingleOrDefault();
                        var lop = db.lops.Where(x => x.idlop == idlop).SingleOrDefault();
                        var idmon = lop.idmon;

                        var CountTrungLich = (from h in db.hocs
                                              join l in db.lops on h.idlop equals l.idlop
                                              where h.idsv == idsv && l.thu == lop.thu && !(l.tietbd > lop.tietkt || l.tietkt < lop.tietbd)
                                              select h).Count();

                        if (CountTrungLich != 0)
                        {
                            err = "Lớp " + lop.malop + ": trùng lịch";
                        }
                        else
                        {
                            var CountMonDaHoc = (from h in db.hocs
                                                 join l in db.lops on h.idlop equals l.idlop
                                                 where h.idsv == idsv && l.idmon == lop.idmon && (h.diemtb >= 5 || h.diemtb == null)
                                                 select h).Count();
                            if (CountMonDaHoc != 0)
                            {
                                err = "Lớp " + lop.malop + ": môn đã học";
                            }
                            else
                            {
                                var idmontrc = db.mons.Where(x => x.idmon == lop.idmon).Select(x => x.idmontruoc).SingleOrDefault();

                                if (idmontrc != null)
                                {
                                    var CountMonTrc = (from h in db.hocs
                                                       join l in db.lops on h.idlop equals l.idlop
                                                       where h.idsv == idsv && l.idmon == idmontrc
                                                       select h).Count();
                                    if (CountMonTrc == 0)
                                    {
                                        err = "Lớp " + lop.malop + ": chưa học môn học trước";
                                    }
                                }
                            }
                        }
                        if (err != "")
                        {
                            listerr.Add(err);
                        }
                        else
                        {
                            var hoc = new hoc() { idlop = idlop, idsv = idsv, sinhvien = sinhvien, lop = lop };
                            db.hocs.Add(hoc);
                            await db.SaveChangesAsync();
                        }
                    }

                    if (listerr.Count != 0)
                    {
                        return Json(new
                        {
                            status = listerr
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = true
                        });
                    }
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
        public async Task<ActionResult> DeleteHoc(string listid)
        {
            var db = new MyDatabaseEntities9();

            var list = new JavaScriptSerializer().Deserialize<List<int>>(listid);
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                foreach (var idlop in list)
                {
                    try
                    {
                        var h = db.hocs.Where(x => x.idlop == idlop && x.idsv == idsv).SingleOrDefault();
                        db.hocs.Remove(h);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        ViewData["error"] = e.ToString();
                        return View();
                    }
                }
                return RedirectToRoute("huydkhp");
            }
            else
            {
                return RedirectToRoute("/");
            }
        }
    }
}