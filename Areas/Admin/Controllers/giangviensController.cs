using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDAA1;

namespace ProjectDAA1.Areas.Admin.Controllers
{
    public class giangviensController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/giangviens
        public async Task<ActionResult> Index()
        {
            var giangviens = db.giangviens.Include(g => g.khoa);
            return View(await giangviens.ToListAsync());
        }

        // GET: Admin/giangviens/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = await db.giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // GET: Admin/giangviens/Create
        public ActionResult Create()
        {
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa");
            return View();
        }

        // POST: Admin/giangviens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "magv,hoten,gioitinh,ngaysinh,diachi,quequan,sdt,capbac,email,ngayvaolam,makhoa")] giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                if (giangvien.ngaysinh > giangvien.ngayvaolam)
                {
                    ModelState.AddModelError("ngaysinh", "ngày sinh lớn hơn ngày vào làm");
                    return View(giangvien);
                }
                int yearvaolam = giangvien.ngayvaolam.Year;
                int yearngaysinh = giangvien.ngaysinh.Year;
                if (yearvaolam - yearngaysinh == 18)
                {
                    ModelState.AddModelError("ngaysinh", "chưa đủ 18 tuổi");
                    return View(giangvien);
                }

                if (giangvien.ngayvaolam > DateTime.Now)
                {
                    ModelState.AddModelError("ngayvaolam", "Ngày vào làm lớn hơn ngày hiện tại");
                    return View(giangvien);
                }

                if (giangvien.ngaysinh > DateTime.Now)
                {
                    ModelState.AddModelError("ngaysinh", "Ngày sinh lớn hơn ngày hiện tại");
                    return View(giangvien);
                }

                else
                {
                    db.giangviens.Add(giangvien);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", giangvien.makhoa);
            return View(giangvien);
        }

        // GET: Admin/giangviens/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = await db.giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", giangvien.makhoa);
            return View(giangvien);
        }

        // POST: Admin/giangviens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "magv,hoten,gioitinh,ngaysinh,diachi,quequan,sdt,capbac,email,ngayvaolam,makhoa")] giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                if (giangvien.ngaysinh > giangvien.ngayvaolam)
                {
                    ModelState.AddModelError("ngaysinh", "ngày sinh lớn hơn ngày vào làm");
                    return View(giangvien);
                }
                int yearvaolam = giangvien.ngayvaolam.Year;
                int yearngaysinh = giangvien.ngaysinh.Year;
                if (yearvaolam - yearngaysinh == 18)
                {
                    ModelState.AddModelError("ngaysinh", "chưa đủ 18 tuổi");
                    return View(giangvien);
                }

                if (giangvien.ngayvaolam > DateTime.Now)
                {
                    ModelState.AddModelError("ngayvaolam", "Ngày vào làm lớn hơn ngày hiện tại");
                    return View(giangvien);
                }

                if (giangvien.ngaysinh > DateTime.Now)
                {
                    ModelState.AddModelError("ngaysinh", "Ngày sinh lớn hơn ngày hiện tại");
                    return View(giangvien);
                }
                else
                {
                    db.Entry(giangvien).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", giangvien.makhoa);
            return View(giangvien);
        }

        // GET: Admin/giangviens/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = await db.giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // POST: Admin/giangviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            giangvien giangvien = await db.giangviens.FindAsync(id);
            db.giangviens.Remove(giangvien);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
