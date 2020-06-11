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
    public class lopsController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/lops
        public async Task<ActionResult> Index()
        {
            var lops = db.lops.Include(l => l.dangkyhocphan).Include(l => l.giangvien).Include(l => l.khoa).Include(l => l.mon);
            return View(await lops.ToListAsync());
        }

        // GET: Admin/lops/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = await db.lops.FindAsync(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // GET: Admin/lops/Create
        public ActionResult Create()
        {
            ViewBag.madkhp = new SelectList(db.dangkyhocphans, "madkhp", "namhoc");
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten");
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa");
            ViewBag.mamon = new SelectList(db.mons, "mamon", "tenmon");
            return View();
        }

        // POST: Admin/lops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "malop,magv,mamon,thu,tietbd,tietkt,madkhp,makhoa")] lop lop)
        {
            if (ModelState.IsValid)
            {
                db.lops.Add(lop);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.madkhp = new SelectList(db.dangkyhocphans, "madkhp", "namhoc", lop.madkhp);
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lop.magv);
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", lop.makhoa);
            ViewBag.mamon = new SelectList(db.mons, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // GET: Admin/lops/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = await db.lops.FindAsync(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            ViewBag.madkhp = new SelectList(db.dangkyhocphans, "madkhp", "namhoc", lop.madkhp);
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lop.magv);
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", lop.makhoa);
            ViewBag.mamon = new SelectList(db.mons, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // POST: Admin/lops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "malop,magv,mamon,thu,tietbd,tietkt,madkhp,makhoa")] lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.madkhp = new SelectList(db.dangkyhocphans, "madkhp", "namhoc", lop.madkhp);
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lop.magv);
            ViewBag.makhoa = new SelectList(db.khoas, "makhoa", "tenkhoa", lop.makhoa);
            ViewBag.mamon = new SelectList(db.mons, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // GET: Admin/lops/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = await db.lops.FindAsync(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Admin/lops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            lop lop = await db.lops.FindAsync(id);
            db.lops.Remove(lop);
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
