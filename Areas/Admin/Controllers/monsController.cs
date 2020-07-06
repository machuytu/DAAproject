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
using ProjectDAA1.Controllers;

namespace ProjectDAA1.Areas.Admin.Controllers
{
    public class monsController : BaseController
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/mons
        public async Task<ActionResult> Index()
        {
            var mons = db.mons.Include(m => m.mon2);
            return View(await mons.ToListAsync());
        }

        // GET: Admin/mons/Details/5
        public async Task<ActionResult> Details(int? id)
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
            ViewBag.idmontruoc = new SelectList(db.mons, "idmon", "tenvama");
            return View();
        }

        // POST: Admin/mons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idmon,tenmon,mamon,idmontruoc,hsqt,hsth,hsgk,hsck,sotc,tclt,tcth")] mon mon)
        {
            if (ModelState.IsValid)
            {
                if (mon.hsck + mon.hsgk + mon.hsqt + mon.hsth != 1)
                {
                    ViewBag.err = "Tổng hệ số điểm phải bằng 1";
                }
                else if (mon.hsck < mon.hsgk || mon.hsck < mon.hsqt || mon.hsck < mon.hsth)
                {
                    ViewBag.err = "Hệ số cuối kỳ không thể bé hơn các hệ số khác";
                }
                else
                {
                    db.mons.Add(mon);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.idmontruoc = new SelectList(db.mons, "idmon", "tenvama", mon.idmontruoc);
            return View(mon);
        }

        // GET: Admin/mons/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.idmontruoc = new SelectList(db.mons, "idmon", "tenvama", mon.idmontruoc);
            return View(mon);
        }

        // POST: Admin/mons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idmon,tenmon,mamon,idmontruoc,hsqt,hsth,hsgk,hsck,sotc,tclt,tcth")] mon mon)
        {
            if (ModelState.IsValid)
            {
                if (mon.hsck + mon.hsgk + mon.hsqt + mon.hsth != 1)
                {
                    ViewBag.err = "Tổng hệ số điểm phải bằng 1";
                }
                else if (mon.hsck < mon.hsgk || mon.hsck < mon.hsqt || mon.hsck < mon.hsth)
                {
                    ViewBag.err = "Hệ số cuối kỳ không thể bé hơn các hệ số khác";
                }
                else
                {
                    db.Entry(mon).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.idmontruoc = new SelectList(db.mons, "idmon", "tenvama", mon.idmontruoc);
            return View(mon);
        }

        // GET: Admin/mons/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
        public async Task<ActionResult> DeleteConfirmed(int id)
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
