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
    public class lopcnsController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/lopcns
        public async Task<ActionResult> Index()
        {
            var lopcns = db.lopcns.Include(l => l.giangvien).Include(l => l.khoa);
            return View(await lopcns.ToListAsync());
        }

        // GET: Admin/lopcns/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = await db.lopcns.FindAsync(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            return View(lopcn);
        }

        // GET: Admin/lopcns/Create
        public ActionResult Create()
        {
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten");
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa");
            return View();
        }

        // POST: Admin/lopcns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "malopcn,magv,makhoa,nienkhoa")] lopcn lopcn)
        {
            if (ModelState.IsValid)
            {
                db.lopcns.Add(lopcn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lopcn.magv);
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", lopcn.makhoa);
            return View(lopcn);
        }

        // GET: Admin/lopcns/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = await db.lopcns.FindAsync(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lopcn.magv);
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", lopcn.makhoa);
            return View(lopcn);
        }

        // POST: Admin/lopcns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "malopcn,magv,makhoa,nienkhoa")] lopcn lopcn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lopcn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lopcn.magv);
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", lopcn.makhoa);
            return View(lopcn);
        }

        // GET: Admin/lopcns/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = await db.lopcns.FindAsync(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            return View(lopcn);
        }

        // POST: Admin/lopcns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            lopcn lopcn = await db.lopcns.FindAsync(id);
            db.lopcns.Remove(lopcn);
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
