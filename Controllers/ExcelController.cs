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
    public class ExcelController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();
        public ExcelController()
        {
            
        }
        [HttpGet]
        public ActionResult Export(int id)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var data = new DataTable();
            //data.Columns.Add("ID", typeof(int));
            //data.Columns.Add("FirstName", typeof(string));
           // data.Columns.Add("LastName", typeof(string));

           // data.Rows.Add(1);
           
            var stream = new MemoryStream();
            int count;
            string malop_temp;
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add($"id");
                sheet.Cells["A1:H99"].Style.Font.Name = "Times New Roman";
                sheet.Cells["A1:C1"].Merge = true;
                sheet.Column(3).Width = 25;
                sheet.Column(1).Width = 5.33;
                sheet.Column(2).Width = 11.67;
                sheet.Column(5).Width = 20;
                sheet.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:C1"].Value = "TRƯỜNG ĐH CÔNG NGHỆ THÔNG TIN";
                sheet.Cells["A2:C2"].Merge = true;
                sheet.Cells["A2:C2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A2:C2"].Style.Font.Bold = true;
                sheet.Cells["A2:C2"].Value = "PHÒNG ĐÀO TẠO ĐẠI HỌC";
                sheet.Cells["A4:D4"].Merge = true;
                sheet.Cells["A4:D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A4:D4"].Style.Font.Bold = true;
                sheet.Cells["A4:D4"].Style.Font.Size=16;
                sheet.Cells["A4:D4"].Value = "DANH SÁCH LỚP";
                sheet.Cells["A5:C5"].Merge = true;
                sheet.Cells["A5:C5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells["A5:C5"].Style.Font.Bold = true;
                sheet.Cells["A5:C5"].Value = "HỌC KÌ:";
                sheet.Cells["D5:E5"].Merge = true;
                sheet.Cells["D5:E5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells["D5:E5"].Style.Font.Bold = true;
                sheet.Cells["D5:E5"].Value = "Năm học:";
                sheet.Cells["A6:C6"].Merge = true;
                sheet.Cells["A6:C6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells["A6:C6"].Style.Font.Bold = true;
                sheet.Cells["A6:C6"].Value = "Môn học:";
                sheet.Cells["D6:E6"].Merge = true;
                sheet.Cells["D6:E6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells["D6:E6"].Style.Font.Bold = true;
                sheet.Cells["D6:E6"].Value = "Lớp:";
                sheet.Cells["A7:C7"].Merge = true;
                sheet.Cells["A7:C7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells["A7:C7"].Style.Font.Bold = true;
                sheet.Cells["A7:C7"].Value = "Giảng viên:";
                sheet.Cells["D7:E7"].Merge = true;
                sheet.Cells["D7:E7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sheet.Cells["D7:E7"].Style.Font.Bold = true;
                sheet.Cells["D7:E7"].Value = "Mã giảng viên:";

                sheet.Row(9).Height = 41.13;
                sheet.Cells["A9:D9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A9:D9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet.Cells["A9:D9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A9:D9"].Style.Fill.BackgroundColor.SetColor(0, 186, 248,255);
                sheet.Cells["A9:D9"].Style.Font.Bold = true;
                sheet.Cells["A9:D9"].Style.Border.Left.Style= ExcelBorderStyle.Thin;
                sheet.Cells["A9:D9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A9:D9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A9:D9"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sheet.Cells["A9:A9"].Value = "STT";
                sheet.Cells["B9:B9"].Value = "Mã số SV";
                sheet.Cells["C9:C9"].Value = "Họ và tên sinh viên";
                sheet.Cells["D9:D9"].Value = "Ghi chú";
                var query = from a in db.dangkyhocphans
                            join b in db.lops on a.iddkhp equals b.iddkhp
                            join e in db.giangviens on b.idgv equals e.idgv
                            join f in db.mons on b.idmon equals f.idmon
                            join c in db.hocs on b.idlop equals c.idlop
                            join d in db.sinhviens on c.idsv equals d.idsv
                            //join b in db.sinhviens on a.idsv equals b.idsv

                            where b.idlop == id
                            select new
                            {
                                Hocki = a.hocky,
                                Namhoc = a.namhoc,
                                Masogv = e.magv,
                                TenGv = e.hoten,
                                TenMon = f.tenmon,
                                Masosv = d.masv,
                                TenSv = d.hoten,
                                Lop = b.malop
                            };
                
                count = query.Count();
                if(count < 1)
                {
                    return new EmptyResult();
                }
                int i = 1;
                int j = 10;
                count = count + 10 - 1;
                string chuoi = $"A10:D{count}";
                var query2 = query.ToList();

               malop_temp = query2[0].Lop;
               if(count >= 10)
                {
                    sheet.Cells[chuoi].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[chuoi].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[chuoi].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[chuoi].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[chuoi].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheet.Cells[chuoi].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    foreach (var item in query)
                    {
                        sheet.Row(j).Height = 30;
                        string stt = $"A{j}:A{j}";
                        string masv = $"B{j}:B{j}";
                        string tensv = $"C{j}:C{j}";
                        sheet.Cells[stt].Value = i;
                        sheet.Cells["A5:C5"].Value = $"HỌC KÌ: {item.Hocki}";
                        sheet.Cells["D5:E5"].Value = $"Năm học: {item.Namhoc}";
                        sheet.Cells["A6:C6"].Value = $"Môn học: {item.TenMon}";
                        sheet.Cells["D6:E6"].Value = $"Lớp: {item.Lop}";
                        sheet.Cells["A7:C7"].Value = $"Giảng viên: {item.TenGv}";
                        sheet.Cells["D7:E7"].Value = $"Mã giảng viên: {item.Masogv}";
                        sheet.Cells[masv].Value = item.Masosv;
                        sheet.Cells[tensv].Value = item.TenSv;
                        i++;
                        j++;
                    }
                    sheet.Name = malop_temp;
                    //sheet.Cells.LoadFromDataTable(data, true);
                    package.Save();
                }
                
            }
            stream.Position = 0;
            
            var tenfile = $"{malop_temp}_{DateTime.Now}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",tenfile);
        }
        // GET: Excel
        public ActionResult Index()
        {
            return View();
        }
    }
}