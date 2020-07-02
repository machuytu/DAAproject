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
        public async Task<ActionResult> Details(int? id)
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
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "tenvama");
            ViewBag.idsv = new SelectList(db.sinhviens, "idsv", "tenvama");
            return View();
        }

        // POST: Admin/taikhoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idtk,matk,password,nhom,idsv,idgv")] taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.taikhoans.Add(taikhoan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "tenvama", taikhoan.idgv);
            ViewBag.idsv = new SelectList(db.sinhviens, "idsv", "tenvama", taikhoan.idsv);
            return View(taikhoan);
        }

        // GET: Admin/taikhoans/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", taikhoan.idgv);
            ViewBag.idsv = new SelectList(db.sinhviens, "idsv", "hoten", taikhoan.idsv);
            return View(taikhoan);
        }

        // POST: Admin/taikhoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idtk,matk,password,nhom,idsv,idgv")] taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taikhoan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", taikhoan.idgv);
            ViewBag.idsv = new SelectList(db.sinhviens, "idsv", "hoten", taikhoan.idsv);
            return View(taikhoan);
        }

        // GET: Admin/taikhoans/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
        public async Task<ActionResult> DeleteConfirmed(int id)
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
