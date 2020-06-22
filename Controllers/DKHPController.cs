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
            var db = new MyDatabaseEntities9();
            var listerr = new List<string>();
            var list = new JavaScriptSerializer().Deserialize<List<int>>(listid);
            var listmaloi = new List<string>();
            var ma = "";
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                foreach (var idlop in list)
                {
                    try
                    {
                        var sinhvien = db.sinhviens.Where(x => x.idsv == idsv).SingleOrDefault();
                        var lop = db.lops.Where(x => x.idlop == idlop).SingleOrDefault();
                        var idmon = db.lops.Where(x => x.idlop == idlop).Select(x => x.idmon);
                        var h = new hoc() { idlop = idlop, idsv = idsv, sinhvien = sinhvien, lop = lop };
                        //ma = lop.malop;
                        db.hocs.Add(h);
                        await db.SaveChangesAsync();

                    }
                    catch (DbUpdateException e)
                    {
                        //listmaloi.Add(ma);
                        //ViewData["error"] = e.ToString();
                        var err = e.InnerException.InnerException.Message.ToString();
                        string[] loi = err.Split('/');
                        listerr.Add(loi[0]);
                       
                        //throw;
                    }
                    //finally
                    //{
                        
                    //}
                }
                if (listmaloi.Count != 0)
                {
                    return Json(new
                    {
                        status = listmaloi
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