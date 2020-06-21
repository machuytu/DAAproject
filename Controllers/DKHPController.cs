using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public async Task<ActionResult> AddHoc(string listid)
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
                        var sinhvien = db.sinhviens.Where(x => x.idsv == idsv).SingleOrDefault();
                        var lop = db.lops.Where(x => x.idlop == idlop).SingleOrDefault();

                        var h = new hoc() { idlop = idlop, idsv = idsv, sinhvien = sinhvien, lop = lop };
                        db.hocs.Add(h);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        ViewData["error"] = e.ToString();
                        return RedirectToRoute("dkhp");
                    }
                }
                return RedirectToRoute("dkhp");
            }  
            else
            {
                return RedirectToRoute("/");
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