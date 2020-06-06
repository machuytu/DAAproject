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
    public class dangkyhocphansController : Controller
    {
        private MyDatabaseEntities8 db = new MyDatabaseEntities8();

        // GET: Admin/dangkyhocphans
        public ActionResult Index()
        {
            return View(db.dangkyhocphans.ToList());
        }

        // GET: Admin/dangkyhocphans/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkyhocphan dangkyhocphan = db.dangkyhocphans.Find(id);
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
        public ActionResult Create([Bind(Include = "madkhp,hocky,namhoc,thoigianbd,thoigiankt")] dangkyhocphan dangkyhocphan)
        {
            if (ModelState.IsValid)
            {
                db.dangkyhocphans.Add(dangkyhocphan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dangkyhocphan);
        }

        // GET: Admin/dangkyhocphans/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkyhocphan dangkyhocphan = db.dangkyhocphans.Find(id);
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
        public ActionResult Edit([Bind(Include = "madkhp,hocky,namhoc,thoigianbd,thoigiankt")] dangkyhocphan dangkyhocphan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dangkyhocphan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dangkyhocphan);
        }

        // GET: Admin/dangkyhocphans/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkyhocphan dangkyhocphan = db.dangkyhocphans.Find(id);
            if (dangkyhocphan == null)
            {
                return HttpNotFound();
            }
            return View(dangkyhocphan);
        }

        // POST: Admin/dangkyhocphans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            dangkyhocphan dangkyhocphan = db.dangkyhocphans.Find(id);
            db.dangkyhocphans.Remove(dangkyhocphan);
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
