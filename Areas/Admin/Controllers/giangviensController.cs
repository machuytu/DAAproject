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
using System.IO;
using ProjectDAA1.Controllers;

namespace ProjectDAA1.Areas.Admin.Controllers
{
    public class giangviensController : AdminAuthController
    {

        // GET: Admin/giangviens
        public async Task<ActionResult> Index()
        {
            var giangviens = db.giangviens.Include(g => g.khoa);
            return View(await giangviens.ToListAsync());
        }

        // GET: Admin/giangviens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = await db.giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // GET: Admin/giangviens/Create
        public ActionResult Create()
        {
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa");
            return View();
        }

        // POST: Admin/giangviens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HttpPostedFileBase file, giangvien giangvien)
        {
            if (ModelState.IsValid && file.ContentLength > 0)
            {
                if (giangvien.ngayvaolam > giangvien.ngaysinh)
                {
                    ModelState.AddModelError("ngayvaolam", "Ngày sinh phải nhỏ hơn ngày vào làm");
                    return View(giangvien);
                }
                else
                {
                    var gv = new giangvien();
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Assets/images"), _FileName);
                    giangvien.hinhanh = "/Assets/images/" + _FileName;
                    gv = giangvien;
                    db.giangviens.Add(gv);
                    db.giangviens.Add(giangvien);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "File Uploaded Fail!!";
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", giangvien.idkhoa);
            return View(giangvien);
        }

        // GET: Admin/giangviens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = await db.giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", giangvien.idkhoa);
            return View(giangvien);
        }

        // POST: Admin/giangviens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HttpPostedFileBase file, giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                if (giangvien.ngayvaolam > giangvien.ngaysinh)
                {
                    ModelState.AddModelError("ngayvaolam", "Ngày sinh phải nhỏ hơn ngày vào làm");
                    return View(giangvien);
                }
                else
                {
                    if (file != null)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("/Assets/images"), _FileName);
                        giangvien.hinhanh = "/Assets/images/" + _FileName;
                        file.SaveAs(_path);
                    }
                    db.Entry(giangvien).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", giangvien.idkhoa);
            return View(giangvien);
        }

        // GET: Admin/giangviens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = await db.giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // POST: Admin/giangviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            giangvien giangvien = await db.giangviens.FindAsync(id);
            db.giangviens.Remove(giangvien);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Deleteuser(int id)
        {
            try
            {
                giangvien giangvien = await db.giangviens.FindAsync(id);
                db.giangviens.Remove(giangvien);
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
