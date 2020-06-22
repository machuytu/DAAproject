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
    public class hocsController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/hocs
        public async Task<ActionResult> Index()
        {
            var hocs = db.hocs.Include(h => h.lop).Include(h => h.sinhvien);
            return View(await hocs.ToListAsync());
        }

        // GET: Admin/hocs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoc hoc = await db.hocs.FindAsync(id);
            if (hoc == null)
            {
                return HttpNotFound();
            }
            return View(hoc);
        }

        // GET: Admin/hocs/Create
        public ActionResult Create()
        {
            List<lop> dbc = db.lops.Include(h => h.mon).ToList();
            SelectList ahihi = new SelectList(dbc, "idlop", "mon.tenvama");

            ViewBag.idlop = ahihi;
            ViewBag.idsv = new SelectList(db.sinhviens, "idsv", "hoten");
            return View();
        }

        // POST: Admin/hocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idhoc,idlop,idsv,diemqt,diemth,diemgk,diemck,diemtb")] hoc hoc)
        {
            if (ModelState.IsValid)
            {
                db.hocs.Add(hoc);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idlop = new SelectList(db.lops, "idlop", "malop", hoc.idlop);
            ViewBag.idsv = new SelectList(db.sinhviens, "idsv", "hoten", hoc.idsv);
            return View(hoc);
        }

        // GET: Admin/hocs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoc hoc = await db.hocs.FindAsync(id);
            if (hoc == null)
            {
                return HttpNotFound();
            }
            List<lop> dbc = db.lops.Include(h => h.mon).ToList();
            SelectList ahihi = new SelectList(dbc, "malop", "mon.tenvama");

            ViewBag.malop = ahihi;
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten");

            return View(hoc);
        }

        // POST: Admin/hocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idhoc,idlop,idsv,diemqt,diemth,diemgk,diemck,diemtb")] hoc hoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idlop = new SelectList(db.lops, "idlop", "malop", hoc.idlop);
            ViewBag.idsv = new SelectList(db.sinhviens, "idsv", "hoten", hoc.idsv);
            return View(hoc);
        }

        // GET: Admin/hocs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hoc hoc = await db.hocs.FindAsync(id);
            if (hoc == null)
            {
                return HttpNotFound();
            }
            return View(hoc);
        }

        // POST: Admin/hocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            hoc hoc = await db.hocs.FindAsync(id);
            db.hocs.Remove(hoc);
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
