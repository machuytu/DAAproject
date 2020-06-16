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
    public class sinhviensController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/sinhviens
        public async Task<ActionResult> Index()
        {
            var sinhviens = db.sinhviens.Include(s => s.lopcn);
            return View(await sinhviens.ToListAsync());
        }

        // GET: Admin/sinhviens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = await db.sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // GET: Admin/sinhviens/Create
        public ActionResult Create()
        {
            ViewBag.idlopcn = new SelectList(db.lopcns, "idlopcn", "malopcn");
            return View();
        }

        // POST: Admin/sinhviens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idsv,hoten,masv,gioitinh,ngaysinh,idlopcn,diachi,quequan,sdt,bachoc")] sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                db.sinhviens.Add(sinhvien);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idlopcn = new SelectList(db.lopcns, "idlopcn", "malopcn", sinhvien.idlopcn);
            return View(sinhvien);
        }

        // GET: Admin/sinhviens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = await db.sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.idlopcn = new SelectList(db.lopcns, "idlopcn", "malopcn", sinhvien.idlopcn);
            return View(sinhvien);
        }

        // POST: Admin/sinhviens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idsv,hoten,masv,gioitinh,ngaysinh,idlopcn,diachi,quequan,sdt,bachoc")] sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhvien).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idlopcn = new SelectList(db.lopcns, "idlopcn", "malopcn", sinhvien.idlopcn);
            return View(sinhvien);
        }

        // GET: Admin/sinhviens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = await db.sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // POST: Admin/sinhviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            sinhvien sinhvien = await db.sinhviens.FindAsync(id);
            db.sinhviens.Remove(sinhvien);
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
