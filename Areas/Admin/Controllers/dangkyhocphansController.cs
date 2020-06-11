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
    public class dangkyhocphansController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/dangkyhocphans
        public async Task<ActionResult> Index()
        {
            return View(await db.dangkyhocphans.ToListAsync());
        }

        // GET: Admin/dangkyhocphans/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkyhocphan dangkyhocphan = await db.dangkyhocphans.FindAsync(id);
            if (dangkyhocphan == null)
            {
                return HttpNotFound();
            }
            return View(dangkyhocphan);
        }

        // GET: Admin/dangkyhocphans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/dangkyhocphans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "madkhp,hocky,namhoc,thoigianbd,thoigiankt")] dangkyhocphan dangkyhocphan)
        {
            if (ModelState.IsValid)
            {
                db.dangkyhocphans.Add(dangkyhocphan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(dangkyhocphan);
        }

        // GET: Admin/dangkyhocphans/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkyhocphan dangkyhocphan = await db.dangkyhocphans.FindAsync(id);
            if (dangkyhocphan == null)
            {
                return HttpNotFound();
            }
            return View(dangkyhocphan);
        }

        // POST: Admin/dangkyhocphans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "madkhp,hocky,namhoc,thoigianbd,thoigiankt")] dangkyhocphan dangkyhocphan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dangkyhocphan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dangkyhocphan);
        }

        // GET: Admin/dangkyhocphans/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkyhocphan dangkyhocphan = await db.dangkyhocphans.FindAsync(id);
            if (dangkyhocphan == null)
            {
                return HttpNotFound();
            }
            return View(dangkyhocphan);
        }

        // POST: Admin/dangkyhocphans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            dangkyhocphan dangkyhocphan = await db.dangkyhocphans.FindAsync(id);
            db.dangkyhocphans.Remove(dangkyhocphan);
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
