using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAA1.Controllers
{
    public class StudentController : AuthSVController
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> thongtincanhan()
        {
            var result = db.sinhviens.Where(x => x.idsv == idsv).SingleOrDefault();
            sinhvien sinhvien = await db.sinhviens.FindAsync(idsv);
            return View(sinhvien);
        }
        public async Task<ActionResult> suathongtin(int id)
        {
            var result = db.sinhviens.Where(x => x.idsv == idsv).SingleOrDefault();
            sinhvien sinhvien = await db.sinhviens.FindAsync(idsv);
            ViewBag.idlopcn = new SelectList(db.lopcns, "idlopcn", "malopcn");
            return View(sinhvien);
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
            taikhoan taikhoan = db.taikhoans.Where(x => x.idsv == idsv).SingleOrDefault();
            return View(taikhoan);
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