using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDAA1.Models;

namespace ProjectDAA1.Areas.Admin.Controllers
{
    public class monsController : Controller
    {
        private MyDatabaseEntities8 db = new MyDatabaseEntities8();

        // GET: Admin/mons
        public ActionResult Index()
        {
            return View(db.mons.ToList());
        }

        // GET: Admin/mons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = db.mons.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // GET: Admin/mons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/mons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mamon,tenmon,hsgk,hsck,sotiet,sotc")] mon mon)
        {
            if (ModelState.IsValid)
            {
                db.mons.Add(mon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mon);
        }

        // GET: Admin/mons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = db.mons.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // POST: Admin/mons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mamon,tenmon,hsgk,hsck,sotiet,sotc")] mon mon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mon);
        }

        // GET: Admin/mons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mon mon = db.mons.Find(id);
            if (mon == null)
            {
                return HttpNotFound();
            }
            return View(mon);
        }

        // POST: Admin/mons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            mon mon = db.mons.Find(id);
            db.mons.Remove(mon);
            db.SaveChanges();
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
