using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAA1.Controllers
{
    public class TeacherController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();
        // GET: Teacher

        public ActionResult dsLop()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            var id = session.idgv;
            var result = db.lops.Where(x => x.idgv == id);
            return View(result);
        }

        public ActionResult dsLopCN()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            var id = session.idgv;
            var result = db.lopcns.Where(x => x.idgv == id);
            return View(result);
        }

        public ActionResult dsSVLopCN(int id)
        {
            var result = db.sinhviens.Where(x => x.idlopcn == id);
            return View(result);
        }

        public ActionResult dsSVLop(int id)
        {
            //var result = db.hocs.Where(x => x.idlop == id).ToList();
            //var result = (from h in db.hocs join sv in db.sinhviens on h.idsv equals sv.idsv
            //              where h.idlop == id select sv).ToList();
            var result = db.hocs.Where(x => x.idlop == id).Select(x => x.sinhvien);
            return View(result);
        }
    }
}