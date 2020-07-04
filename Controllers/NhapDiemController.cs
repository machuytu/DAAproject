using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using GemBox.Spreadsheet;
using System.IO;
using OfficeOpenXml;
using ProjectDAA1.Models;
using System.Drawing;
using OfficeOpenXml.Style;

namespace ProjectDAA1.Controllers
{
    public class NhapDiemController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();
        
        // GET: NhapDiem
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file,int id,string diem)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ViewBag.Message = "Thành công:";
            ViewData["Trangthai"] = ViewBag.Message;
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                   
                    string _path = Path.Combine(Server.MapPath("~/Uploadfile/"), _FileName);
                    file.SaveAs(_path);
                    var package = new OfficeOpenXml.ExcelPackage(new FileInfo(_path));
                    var workSheet = package.Workbook.Worksheets[0];
                    int count;

                    for(count = 10;workSheet.Cells[$"A{count},A{count}"].Value!=null; count++)
                    {
                        var masinhvien = (string)(workSheet.Cells[$"B{count},B{count}"].Value);
                        var diemexcel = (double)(workSheet.Cells[$"D{count},D{count}"].Value);
                        var query = (from c in db.sinhviens where c.masv == masinhvien select c.idsv).FirstOrDefault();
                        var hoc = (from a in db.hocs
                                   where a.idlop == id && a.idsv == query
                                   select a).FirstOrDefault();
                        if(hoc!=null)
                        {
                            if (diem == "diemqt")
                            {
                                hoc.diemqt = diemexcel;
                            }
                            else if (diem == "diemth")
                            {
                                hoc.diemth = diemexcel;
                            }
                            else if (diem == "diemgk")
                            {
                                hoc.diemgk = diemexcel;
                            }
                            else if (diem == "diemck")
                            {
                                hoc.diemck = diemexcel;
                            }
                            db.SaveChanges();
                        }
                    }
                }
                
                return Redirect("/bangdiem/lop/"+id);
            }
            catch
            {
                ViewBag.Message = "Thất bại!!!";
                return new EmptyResult();
            }
        }
    }
}