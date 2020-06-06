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
    public class lopcnsController : Controller
    {
        private MyDatabaseEntities5 db = new MyDatabaseEntities5();

        // GET: Admin/lopcns
        public ActionResult Index()
        {
            var lopcn = db.lopcn.Include(l => l.giangvien);
            return View(lopcn.ToList());
        }

        // GET: Admin/lopcns/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = db.lopcn.Find(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            return View(lopcn);
        }

        // GET: Admin/lopcns/Create
        public ActionResult Create()
        {
            ViewBag.magv = new SelectList(db.giangvien, "magv", "hoten");
            return View();
        }

        // POST: Admin/lopcns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "malopcn,magv,nienkhoa")] lopcn lopcn)
        {
            if (ModelState.IsValid)
            {
                db.lopcn.Add(lopcn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.magv = new SelectList(db.giangvien, "magv", "hoten", lopcn.magv);
            return View(lopcn);
        }

        // GET: Admin/lopcns/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = db.lopcn.Find(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            ViewBag.magv = new SelectList(db.giangvien, "magv", "hoten", lopcn.magv);
            return View(lopcn);
        }

        // POST: Admin/lopcns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "malopcn,magv,nienkhoa")] lopcn lopcn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lopcn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.magv = new SelectList(db.giangvien, "magv", "hoten", lopcn.magv);
            return View(lopcn);
        }

        // GET: Admin/lopcns/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = db.lopcn.Find(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            return View(lopcn);
        }

        // POST: Admin/lopcns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            lopcn lopcn = db.lopcn.Find(id);
            db.lopcn.Remove(lopcn);
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
