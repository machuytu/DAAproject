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
    public class sinhviensController : BaseController
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
        public async Task<ActionResult> Create(sinhvien sinhvien, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file.ContentLength >0)
            {
                var sv = new sinhvien();
                //sv.bachoc = sinhvien.bachoc;
                //sv.diachi = sinhvien.diachi;
                //sv.gioitinh = sinhvien.gioitinh;
                //sv.hoten = sinhvien.hoten;
                //sv.ngaysinh = sinhvien.ngaysinh;
                //sv.idlopcn = sinhvien.idlopcn;
                //sv.sdt = sinhvien.sdt;
                //sv.quequan = sinhvien.quequan;
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("/Assets/images"), _FileName);
                sinhvien.hinhanh = "/Assets/images/" + _FileName;
                sv = sinhvien;
                db.sinhviens.Add(sv);
                file.SaveAs(_path);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "File Uploaded Fail!!";
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
        public async Task<ActionResult> Edit(sinhvien sinhvien,HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
               
                    if (file != null)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("/Assets/images"), _FileName);
                        sinhvien.hinhanh = "/Assets/images/" + _FileName;
                        file.SaveAs(_path);
                    }
                  try
                {
                    db.Entry(sinhvien).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                    catch (Exception)
                {
                    ViewBag.err = "niên khóa không phù hợp";
                }
                            
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
        [HttpPost]
        public async Task<ActionResult> Deleteuser(int id)
        {
            try
            {
                sinhvien sinhvien = await db.sinhviens.FindAsync(id);
                db.sinhviens.Remove(sinhvien);
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
