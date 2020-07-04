using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAA1.Controllers
{
    public class StudentController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> thongtincanhan()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                var result = db.sinhviens.Where(x => x.idsv == idsv).SingleOrDefault();
                sinhvien sinhvien = await db.sinhviens.FindAsync(idsv);
                return View(sinhvien);
            }
            else
            {
                return RedirectToRoute("login");
            }
        }
        public async Task<ActionResult> suathongtin(int id)
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                var result = db.sinhviens.Where(x => x.idsv == idsv).SingleOrDefault();
                sinhvien sinhvien = await db.sinhviens.FindAsync(idsv);
                ViewBag.idlopcn = new SelectList(db.lopcns, "idlopcn", "malopcn");
                return View(sinhvien);
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> suathongtin(HttpPostedFileBase file, sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Assets/images"), _FileName);
                    sinhvien.hinhanh = "/Assets/images/" + _FileName;
                    file.SaveAs(_path);
                }
                db.Entry(sinhvien).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Redirect("/sinhvien/thongtincanhan");
            }
            ViewBag.idlopcn = new SelectList(db.lopcns, "idlopcn", "malopcn");
            return View(sinhvien);
        }

        public async Task<ActionResult> doimatkhau()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idsv = session.idsv;
                taikhoan taikhoan = db.taikhoans.Where(x => x.idsv == idsv).SingleOrDefault();
                return View(taikhoan);
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> doimatkhau(taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taikhoan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Redirect("/sinhvien/thongtincanhan");
            }
            return View(taikhoan);
        }
    }
}