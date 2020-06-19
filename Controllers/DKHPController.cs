using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ProjectDAA1.Controllers
{
    public class DKHPController : Controller
    {
        // GET: DKHP
        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            var db = new MyDatabaseEntities9();
            var datenow = DateTime.Now;
            var listkhoahoc = db.dangkyhocphans.Where(x => x.thoigianbd <= datenow && x.thoigiankt >= datenow).SingleOrDefault();
            if (listkhoahoc != null)
            {
                var id = listkhoahoc.iddkhp;
                var dsdkhp = db.lops.Where(x => x.iddkhp == id);
                return View(await dsdkhp.ToListAsync());
            }
            else
            {
                return RedirectToRoute("/");
            }
            //var listclass = dsdkhp.Where()
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
                        var h = new hoc() { idlop = idlop,  idsv = idsv};
                        //h.idlop = idlop;
                        //h.idsv = idsv;
                        db.hocs.Add(h);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        ViewData["error"] = e.ToString(); 
                        return View();
                    }
                    
                }
                return View();
            }  
            else
            {
                return RedirectToRoute("/");
            }    
        }
    }
}