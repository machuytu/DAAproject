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
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];

            if (session != null)
            {
                var idsv = session.idsv;
                using (var db = new MyDatabaseEntities9())
                {
                    var iddkhp = db.dangkyhocphans
                        .Where(x => x.thoigianbd <= DateTime.Now && x.thoigiankt >= DateTime.Now)
                        .Select(x => x.iddkhp)
                        .FirstOrDefault();

                    if (iddkhp != 0)
                    {
                        var dslop = db.lops
                            .Where(x => x.iddkhp == iddkhp)
                            .Select(x => new HocPhan
                            {
                                idlop = x.idlop,
                                malop = x.malop,
                                tenmon = x.mon.tenmon,
                                mamon = x.mon.mamon,
                                tengv = x.giangvien.hoten,
                                tclt = x.mon.tclt,
                                tcth = x.mon.tcth,
                                thu = x.thu,
                                tietbd = x.tietbd,
                                tietkt = x.tietkt,
                            });

                        var dslopdadk = db.hocs
                            .Where(x => x.idsv == idsv && x.lop.iddkhp == iddkhp)
                            .Select(x => new HocPhan
                            {
                                idlop = x.lop.idlop,
                                malop = x.lop.malop,
                                tenmon = x.lop.mon.tenmon,
                                mamon = x.lop.mon.mamon,
                                tengv = x.lop.giangvien.hoten,
                                tclt = x.lop.mon.tclt,
                                tcth = x.lop.mon.tcth,
                                thu = x.lop.thu,
                                tietbd = x.lop.tietbd,
                                tietkt = x.lop.tietkt,
                            });

                        foreach (var item in dslop)
                        {
                            var rs = db.hocs.Where(x => x.idlop == item.idlop).Count();
                        }

                        foreach (var item in dslop)
                        {
                            var rs = db.hocs.Where(x => x.idlop == item.idlop).Count();
                        }

                        return View(new DBContext()
                        {
                            dslopchuahoc = dslop.Except(dslopdadk).ToList(),
                            dslopdahoc = dslopdadk.ToList(),
                        });
                    }

                    else
                    {
                        return Redirect("/");
                    }
                }
            }
            else
            {
                return RedirectToRoute("login");
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

                        if ((from h in db.hocs
                             join l in db.lops on h.idlop equals l.idlop
                             where h.idsv == idsv && l.iddkhp == lop.iddkhp && l.thu == lop.thu && !(l.tietbd > lop.tietkt || l.tietkt < lop.tietbd)
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
                    status = false
                });
            }
        }

        // GET: HuyDKHP
        public ActionResult GetHuyDKHP()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];

            if (session != null)
            {
                var idsv = session.idsv;
                using (var db = new MyDatabaseEntities9())
                {
                    var iddkhp = db.dangkyhocphans
                        .Where(x => x.thoigianbd <= DateTime.Now && x.thoigiankt >= DateTime.Now)
                        .Select(x => x.iddkhp)
                        .FirstOrDefault();

                    if (iddkhp != 0)
                    {
                        var dslopdadk = db.hocs
                            .Where(x => x.idsv == idsv && x.lop.iddkhp == iddkhp)
                            .Select(x => new HocPhan
                            {
                                idlop = x.lop.idlop,
                                malop = x.lop.malop,
                                tenmon = x.lop.mon.tenmon,
                                mamon = x.lop.mon.mamon,
                                tengv = x.lop.giangvien.hoten,
                                tclt = x.lop.mon.tclt,
                                tcth = x.lop.mon.tcth,
                                thu = x.lop.thu,
                                tietbd = x.lop.tietbd,
                                tietkt = x.lop.tietkt,
                            });

                        return View(new DBContext()
                        {
                            dslopdahoc = dslopdadk.ToList(),
                        });
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteHoc(string listid)
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
                        var lop = db.lops
                            .Where(x => x.idlop == idlop)
                            .FirstOrDefault();

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
                            var hoc = db.hocs
                                .Where(x => x.idlop == idlop && x.idsv == idsv)
                                .FirstOrDefault();
                            db.hocs.Remove(hoc);
                            await db.SaveChangesAsync();
                        }
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
                var tkb = db.hocs
                    .Where(x => x.idsv == idsv && x.lop.iddkhp == iddkhp)
                    .Select(x => new HocPhan
                    {
                        idlop = x.lop.idlop,
                        malop = x.lop.malop,
                        tenmon = x.lop.mon.tenmon,
                        mamon = x.lop.mon.mamon,
                        tengv = x.lop.giangvien.hoten,
                        tclt = x.lop.mon.tclt,
                        tcth = x.lop.mon.tcth,
                        thu = x.lop.thu,
                        tietbd = x.lop.tietbd,
                        tietkt = x.lop.tietkt,
                    }).ToList();

                return new JsonResult
                {
                    Data = tkb,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                };
            }
        }

        [HttpGet]
        public ActionResult GetKQHT()
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

                    var dskqht = db.hocs
                        .Where(x => x.idsv == idsv)
                        .Select(x => new KQHT()
                        {
                            iddkhp = x.lop.iddkhp,
                            mamon = x.lop.mon.mamon,
                            tenmon = x.lop.mon.tenmon,
                            sotc = x.lop.mon.sotc,
                            hsqt = x.lop.mon.hsqt,
                            hsth = x.lop.mon.hsth,
                            hsgk = x.lop.mon.hsgk,
                            hsck = x.lop.mon.hsck,
                            diemqt = x.diemqt,
                            diemth = x.diemth,
                            diemgk = x.diemgk,
                            diemck = x.diemck,
                            diemtb = x.diemtb,
                        });

                    return View(new DBContext()
                    {
                        dsdkhp = dsdkhp.ToList(),
                        dskqht = dskqht.ToList(),
                    });
                }
            }
            else
            {
                return RedirectToRoute("login");
            }
        }
    }
}