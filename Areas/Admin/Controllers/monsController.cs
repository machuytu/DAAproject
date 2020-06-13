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
    public class monsController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/mons
        public async Task<ActionResult> Index()
        {
            var mons = db.mons.Include(m => m.mon2);
            return View(await mons.ToListAsync());
        }

        // GET: Admin/mons/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = await db.mons.FindAsync(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // GET: Admin/mons/Create
        public ActionResult Create()
        {
            List<SelectListItem> MyOwnerList = new List<SelectListItem>();
            MyOwnerList.AddRange(new SelectList(db.mons, "mamon", "tenmon"));
            MyOwnerList.Insert(0, new SelectListItem { Text = "", Value = "" });
            ViewBag.mamontruoc = MyOwnerList;
            return View();
        }

        // POST: Admin/mons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "mamon,tenmon,hsgk,hsck,sotc,mamontruoc,hsqt,hsth,tclt,tcth")] mon mon)
        {
            if (ModelState.IsValid)
            {
                db.mons.Add(mon);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.mamontruoc = new SelectList(db.mons, "mamon", "tenmon", mon.mamontruoc);
            return View(mon);
        }

        // GET: Admin/mons/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = await db.mons.FindAsync(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            ViewBag.mamontruoc = new SelectList(db.mons, "mamon", "tenmon", mon.mamontruoc);
            return View(mon);
        }

        // POST: Admin/mons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "mamon,tenmon,hsgk,hsck,sotc,mamontruoc,hsqt,hsth,tclt,tcth")] mon mon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mon).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.mamontruoc = new SelectList(db.mons, "mamon", "tenmon", mon.mamontruoc);
            return View(mon);
        }

        // GET: Admin/mons/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = await db.mons.FindAsync(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // POST: Admin/mons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            mon mon = await db.mons.FindAsync(id);
            db.mons.Remove(mon);
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
