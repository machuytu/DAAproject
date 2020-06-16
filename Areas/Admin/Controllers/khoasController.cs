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
    public class khoasController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/khoas
        public async Task<ActionResult> Index()
        {
            var khoas = db.khoas.Include(k => k.giangvien);
            return View(await khoas.ToListAsync());
        }

        // GET: Admin/khoas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khoa khoa = await db.khoas.FindAsync(id);
            if (khoa == null)
            {
                return HttpNotFound();
            }
            return View(khoa);
        }

        // GET: Admin/khoas/Create
        public ActionResult Create()
        {
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten");
            return View();
        }

        // POST: Admin/khoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idkhoa,tenkhoa,makhoa,idgv")] khoa khoa)
        {
            if (ModelState.IsValid)
            {
                db.khoas.Add(khoa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", khoa.idgv);
            return View(khoa);
        }

        // GET: Admin/khoas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khoa khoa = await db.khoas.FindAsync(id);
            if (khoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", khoa.idgv);
            return View(khoa);
        }

        // POST: Admin/khoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idkhoa,tenkhoa,makhoa,idgv")] khoa khoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khoa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", khoa.idgv);
            return View(khoa);
        }

        // GET: Admin/khoas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khoa khoa = await db.khoas.FindAsync(id);
            if (khoa == null)
            {
                return HttpNotFound();
            }
            return View(khoa);
        }

        // POST: Admin/khoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            khoa khoa = await db.khoas.FindAsync(id);
            db.khoas.Remove(khoa);
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
