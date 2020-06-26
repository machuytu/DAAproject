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
        public async Task<ActionResult> Details(int? id)
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
        public async Task<ActionResult> Create(dangkyhocphan dangkyhocphan)
        {
            if (ModelState.IsValid)
            {
                if (dangkyhocphan.thoigianbd > dangkyhocphan.thoigiankt)
                {
                    ModelState.AddModelError("thoigianbd", "Ngày bắt đầu lớn hơn ngày kết thúc");
                    return View(dangkyhocphan);
                }

                else
                {
                    db.dangkyhocphans.Add(dangkyhocphan);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(dangkyhocphan);
        }

        // GET: Admin/dangkyhocphans/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
        public async Task<ActionResult> Edit(dangkyhocphan dangkyhocphan)
        {
            if (ModelState.IsValid)
            {
                if (dangkyhocphan.thoigianbd > dangkyhocphan.thoigiankt)
                {
                    ModelState.AddModelError("thoigianbd", "Ngày bắt đầu lớn hơn ngày kết thúc");
                    return View(dangkyhocphan);
                }
                else
                {
                    db.Entry(dangkyhocphan).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(dangkyhocphan);
        }

        // GET: Admin/dangkyhocphans/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            dangkyhocphan dangkyhocphan = await db.dangkyhocphans.FindAsync(id);
            db.dangkyhocphans.Remove(dangkyhocphan);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Deleteuser(int id)
        {
            try
            {
                dangkyhocphan dangkyhocphan = await db.dangkyhocphans.FindAsync(id);
                db.dangkyhocphans.Remove(dangkyhocphan);
                await db.SaveChangesAsync();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
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
