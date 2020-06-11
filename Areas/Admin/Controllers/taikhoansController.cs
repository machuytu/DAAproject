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
    public class taikhoansController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/taikhoans
        public async Task<ActionResult> Index()
        {
            var taikhoans = db.taikhoans.Include(t => t.giangvien).Include(t => t.sinhvien);
            return View(await taikhoans.ToListAsync());
        }

        // GET: Admin/taikhoans/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taikhoan taikhoan = await db.taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // GET: Admin/taikhoans/Create
        public ActionResult Create()
        {
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten");
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten");
            return View();
        }

        // POST: Admin/taikhoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "matk,password,masv,magv,nhom")] taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.taikhoans.Add(taikhoan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", taikhoan.magv);
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten", taikhoan.masv);
            return View(taikhoan);
        }

        // GET: Admin/taikhoans/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taikhoan taikhoan = await db.taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", taikhoan.magv);
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten", taikhoan.masv);
            return View(taikhoan);
        }

        // POST: Admin/taikhoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "matk,password,masv,magv,nhom")] taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taikhoan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", taikhoan.magv);
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten", taikhoan.masv);
            return View(taikhoan);
        }

        // GET: Admin/taikhoans/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taikhoan taikhoan = await db.taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // POST: Admin/taikhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            taikhoan taikhoan = await db.taikhoans.FindAsync(id);
            db.taikhoans.Remove(taikhoan);
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
