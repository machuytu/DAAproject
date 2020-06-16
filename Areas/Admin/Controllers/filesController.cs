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
    public class filesController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/files
        public async Task<ActionResult> Index()
        {
            var files = db.files.Include(f => f.thongbao);
            return View(await files.ToListAsync());
        }

        // GET: Admin/files/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = await db.files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Admin/files/Create
        public ActionResult Create()
        {
            ViewBag.idtb = new SelectList(db.thongbaos, "idtb", "tieude");
            return View();
        }

        // POST: Admin/files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idfile,filename,extension,note,idtb")] file file)
        {
            if (ModelState.IsValid)
            {
                db.files.Add(file);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idtb = new SelectList(db.thongbaos, "idtb", "tieude", file.idtb);
            return View(file);
        }

        // GET: Admin/files/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = await db.files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.idtb = new SelectList(db.thongbaos, "idtb", "tieude", file.idtb);
            return View(file);
        }

        // POST: Admin/files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idfile,filename,extension,note,idtb")] file file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idtb = new SelectList(db.thongbaos, "idtb", "tieude", file.idtb);
            return View(file);
        }

        // GET: Admin/files/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = await db.files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Admin/files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            file file = await db.files.FindAsync(id);
            db.files.Remove(file);
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
