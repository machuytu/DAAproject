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
        public async Task<ActionResult> Details(int? id)
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
            ViewBag.iddkhp = new SelectList(db.dangkyhocphans, "iddkhp", "namhoc");
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten");
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa");
            ViewBag.idmon = new SelectList(db.mons, "idmon", "tenmon");
            return View();
        }

        // POST: Admin/lops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idlop,malop,idgv,idmon,idkhoa,iddkhp,thu,tietbd,tietkt")] lop lop)
        {
            if (ModelState.IsValid)
            {
                db.lops.Add(lop);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.iddkhp = new SelectList(db.dangkyhocphans, "iddkhp", "namhoc", lop.iddkhp);
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", lop.idgv);
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", lop.idkhoa);
            ViewBag.idmon = new SelectList(db.mons, "idmon", "tenmon", lop.idmon);
            return View(lop);
        }

        // GET: Admin/lops/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.iddkhp = new SelectList(db.dangkyhocphans, "iddkhp", "namhoc", lop.iddkhp);
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", lop.idgv);
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", lop.idkhoa);
            ViewBag.idmon = new SelectList(db.mons, "idmon", "tenmon", lop.idmon);
            return View(lop);
        }

        // POST: Admin/lops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idlop,malop,idgv,idmon,idkhoa,iddkhp,thu,tietbd,tietkt")] lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.iddkhp = new SelectList(db.dangkyhocphans, "iddkhp", "namhoc", lop.iddkhp);
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", lop.idgv);
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", lop.idkhoa);
            ViewBag.idmon = new SelectList(db.mons, "idmon", "tenmon", lop.idmon);
            return View(lop);
        }

        // GET: Admin/lops/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
        public async Task<ActionResult> DeleteConfirmed(int id)
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
