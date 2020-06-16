using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAA1.Controllers
{
    public class TeacherController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();
        // GET: Teacher
        public ActionResult GetClass(int idgv)
        {
            var getallclass = db.lopcns.Where(x => x.idgv == idgv).ToList();
            return View(getallclass);
        }
    }
}