using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAA1.Controllers
{
    public class DKHPController : Controller
    {
        // GET: DKHP
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        public ActionResult GetList(int iddkhp)
        {
            var dsdkhp = db.lops.Where(x=>x.iddkhp ==iddkhp).ToList();
            //var listclass = dsdkhp.Where()
            return View(dsdkhp);
        }
    }
}