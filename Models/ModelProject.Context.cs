﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectDAA1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MyDatabaseEntities8 : DbContext
    {
        public MyDatabaseEntities8()
            : base("name=MyDatabaseEntities8")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<dangkyhocphan> dangkyhocphans { get; set; }
        public virtual DbSet<giangday> giangdays { get; set; }
        public virtual DbSet<giangvien> giangviens { get; set; }
        public virtual DbSet<lop> lops { get; set; }
        public virtual DbSet<lopcn> lopcns { get; set; }
        public virtual DbSet<mon> mons { get; set; }
        public virtual DbSet<sinhvien> sinhviens { get; set; }
        public virtual DbSet<taikhoan> taikhoans { get; set; }
    
        public virtual int AddDKHP(string namhoc, Nullable<int> hocky, Nullable<System.DateTime> thoigianbd, Nullable<System.DateTime> thoigiankt)
        {
            var namhocParameter = namhoc != null ?
                new ObjectParameter("namhoc", namhoc) :
                new ObjectParameter("namhoc", typeof(string));
    
            var hockyParameter = hocky.HasValue ?
                new ObjectParameter("hocky", hocky) :
                new ObjectParameter("hocky", typeof(int));
    
            var thoigianbdParameter = thoigianbd.HasValue ?
                new ObjectParameter("thoigianbd", thoigianbd) :
                new ObjectParameter("thoigianbd", typeof(System.DateTime));
    
            var thoigianktParameter = thoigiankt.HasValue ?
                new ObjectParameter("thoigiankt", thoigiankt) :
                new ObjectParameter("thoigiankt", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddDKHP", namhocParameter, hockyParameter, thoigianbdParameter, thoigianktParameter);
        }
    
        public virtual int AddGiangDay(string masv, string malop)
        {
            var masvParameter = masv != null ?
                new ObjectParameter("masv", masv) :
                new ObjectParameter("masv", typeof(string));
    
            var malopParameter = malop != null ?
                new ObjectParameter("malop", malop) :
                new ObjectParameter("malop", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddGiangDay", masvParameter, malopParameter);
        }
    
        public virtual int AddGV(string magv, string hoten, string gioitinh, Nullable<System.DateTime> ngaysinh, string sdt, string diachi, string quequan, string capbac, string email, Nullable<System.DateTime> ngayvaolam)
        {
            var magvParameter = magv != null ?
                new ObjectParameter("magv", magv) :
                new ObjectParameter("magv", typeof(string));
    
            var hotenParameter = hoten != null ?
                new ObjectParameter("hoten", hoten) :
                new ObjectParameter("hoten", typeof(string));
    
            var gioitinhParameter = gioitinh != null ?
                new ObjectParameter("gioitinh", gioitinh) :
                new ObjectParameter("gioitinh", typeof(string));
    
            var ngaysinhParameter = ngaysinh.HasValue ?
                new ObjectParameter("ngaysinh", ngaysinh) :
                new ObjectParameter("ngaysinh", typeof(System.DateTime));
    
            var sdtParameter = sdt != null ?
                new ObjectParameter("sdt", sdt) :
                new ObjectParameter("sdt", typeof(string));
    
            var diachiParameter = diachi != null ?
                new ObjectParameter("diachi", diachi) :
                new ObjectParameter("diachi", typeof(string));
    
            var quequanParameter = quequan != null ?
                new ObjectParameter("quequan", quequan) :
                new ObjectParameter("quequan", typeof(string));
    
            var capbacParameter = capbac != null ?
                new ObjectParameter("capbac", capbac) :
                new ObjectParameter("capbac", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var ngayvaolamParameter = ngayvaolam.HasValue ?
                new ObjectParameter("ngayvaolam", ngayvaolam) :
                new ObjectParameter("ngayvaolam", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddGV", magvParameter, hotenParameter, gioitinhParameter, ngaysinhParameter, sdtParameter, diachiParameter, quequanParameter, capbacParameter, emailParameter, ngayvaolamParameter);
        }
    
        public virtual int AddLop(string mamon, string magv, string madkhp, Nullable<int> thu, Nullable<int> tietbd)
        {
            var mamonParameter = mamon != null ?
                new ObjectParameter("mamon", mamon) :
                new ObjectParameter("mamon", typeof(string));
    
            var magvParameter = magv != null ?
                new ObjectParameter("magv", magv) :
                new ObjectParameter("magv", typeof(string));
    
            var madkhpParameter = madkhp != null ?
                new ObjectParameter("madkhp", madkhp) :
                new ObjectParameter("madkhp", typeof(string));
    
            var thuParameter = thu.HasValue ?
                new ObjectParameter("thu", thu) :
                new ObjectParameter("thu", typeof(int));
    
            var tietbdParameter = tietbd.HasValue ?
                new ObjectParameter("tietbd", tietbd) :
                new ObjectParameter("tietbd", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddLop", mamonParameter, magvParameter, madkhpParameter, thuParameter, tietbdParameter);
        }
    
        public virtual int AddLopCN(string lop, string magv, Nullable<int> nienkhoa)
        {
            var lopParameter = lop != null ?
                new ObjectParameter("lop", lop) :
                new ObjectParameter("lop", typeof(string));
    
            var magvParameter = magv != null ?
                new ObjectParameter("magv", magv) :
                new ObjectParameter("magv", typeof(string));
    
            var nienkhoaParameter = nienkhoa.HasValue ?
                new ObjectParameter("nienkhoa", nienkhoa) :
                new ObjectParameter("nienkhoa", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddLopCN", lopParameter, magvParameter, nienkhoaParameter);
        }
    
        public virtual int AddMon(string mamon, string tenmon, Nullable<int> sotiet, Nullable<int> sotc, Nullable<double> hsck)
        {
            var mamonParameter = mamon != null ?
                new ObjectParameter("mamon", mamon) :
                new ObjectParameter("mamon", typeof(string));
    
            var tenmonParameter = tenmon != null ?
                new ObjectParameter("tenmon", tenmon) :
                new ObjectParameter("tenmon", typeof(string));
    
            var sotietParameter = sotiet.HasValue ?
                new ObjectParameter("sotiet", sotiet) :
                new ObjectParameter("sotiet", typeof(int));
    
            var sotcParameter = sotc.HasValue ?
                new ObjectParameter("sotc", sotc) :
                new ObjectParameter("sotc", typeof(int));
    
            var hsckParameter = hsck.HasValue ?
                new ObjectParameter("hsck", hsck) :
                new ObjectParameter("hsck", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddMon", mamonParameter, tenmonParameter, sotietParameter, sotcParameter, hsckParameter);
        }
    
        public virtual int AddSV(string hoten, string gioitinh, Nullable<System.DateTime> ngaysinh, string malopcn, string sdt, string diachi, string quequan, string bachoc)
        {
            var hotenParameter = hoten != null ?
                new ObjectParameter("hoten", hoten) :
                new ObjectParameter("hoten", typeof(string));
    
            var gioitinhParameter = gioitinh != null ?
                new ObjectParameter("gioitinh", gioitinh) :
                new ObjectParameter("gioitinh", typeof(string));
    
            var ngaysinhParameter = ngaysinh.HasValue ?
                new ObjectParameter("ngaysinh", ngaysinh) :
                new ObjectParameter("ngaysinh", typeof(System.DateTime));
    
            var malopcnParameter = malopcn != null ?
                new ObjectParameter("malopcn", malopcn) :
                new ObjectParameter("malopcn", typeof(string));
    
            var sdtParameter = sdt != null ?
                new ObjectParameter("sdt", sdt) :
                new ObjectParameter("sdt", typeof(string));
    
            var diachiParameter = diachi != null ?
                new ObjectParameter("diachi", diachi) :
                new ObjectParameter("diachi", typeof(string));
    
            var quequanParameter = quequan != null ?
                new ObjectParameter("quequan", quequan) :
                new ObjectParameter("quequan", typeof(string));
    
            var bachocParameter = bachoc != null ?
                new ObjectParameter("bachoc", bachoc) :
                new ObjectParameter("bachoc", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddSV", hotenParameter, gioitinhParameter, ngaysinhParameter, malopcnParameter, sdtParameter, diachiParameter, quequanParameter, bachocParameter);
        }
    
        public virtual int AddTK(string matk, string password1, string password2, string nhom)
        {
            var matkParameter = matk != null ?
                new ObjectParameter("matk", matk) :
                new ObjectParameter("matk", typeof(string));
    
            var password1Parameter = password1 != null ?
                new ObjectParameter("password1", password1) :
                new ObjectParameter("password1", typeof(string));
    
            var password2Parameter = password2 != null ?
                new ObjectParameter("password2", password2) :
                new ObjectParameter("password2", typeof(string));
    
            var nhomParameter = nhom != null ?
                new ObjectParameter("nhom", nhom) :
                new ObjectParameter("nhom", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddTK", matkParameter, password1Parameter, password2Parameter, nhomParameter);
        }
    
        public virtual int ChangeLopCNSV(string masv, string malopcn)
        {
            var masvParameter = masv != null ?
                new ObjectParameter("masv", masv) :
                new ObjectParameter("masv", typeof(string));
    
            var malopcnParameter = malopcn != null ?
                new ObjectParameter("malopcn", malopcn) :
                new ObjectParameter("malopcn", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ChangeLopCNSV", masvParameter, malopcnParameter);
        }
    
        public virtual int ChangePassword1(string matk, string password1old, string password1new)
        {
            var matkParameter = matk != null ?
                new ObjectParameter("matk", matk) :
                new ObjectParameter("matk", typeof(string));
    
            var password1oldParameter = password1old != null ?
                new ObjectParameter("password1old", password1old) :
                new ObjectParameter("password1old", typeof(string));
    
            var password1newParameter = password1new != null ?
                new ObjectParameter("password1new", password1new) :
                new ObjectParameter("password1new", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ChangePassword1", matkParameter, password1oldParameter, password1newParameter);
        }
    
        public virtual int ChangePassword2(string matk, string password2)
        {
            var matkParameter = matk != null ?
                new ObjectParameter("matk", matk) :
                new ObjectParameter("matk", typeof(string));
    
            var password2Parameter = password2 != null ?
                new ObjectParameter("password2", password2) :
                new ObjectParameter("password2", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ChangePassword2", matkParameter, password2Parameter);
        }
    
        public virtual int DeleteDKHP(string madkhp)
        {
            var madkhpParameter = madkhp != null ?
                new ObjectParameter("madkhp", madkhp) :
                new ObjectParameter("madkhp", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteDKHP", madkhpParameter);
        }
    
        public virtual int DeleteGiangDay(string masv, string malop)
        {
            var masvParameter = masv != null ?
                new ObjectParameter("masv", masv) :
                new ObjectParameter("masv", typeof(string));
    
            var malopParameter = malop != null ?
                new ObjectParameter("malop", malop) :
                new ObjectParameter("malop", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteGiangDay", masvParameter, malopParameter);
        }
    
        public virtual int DeleteGV(string magv)
        {
            var magvParameter = magv != null ?
                new ObjectParameter("magv", magv) :
                new ObjectParameter("magv", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteGV", magvParameter);
        }
    
        public virtual int DeleteLop(string malop)
        {
            var malopParameter = malop != null ?
                new ObjectParameter("malop", malop) :
                new ObjectParameter("malop", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteLop", malopParameter);
        }
    
        public virtual int DeleteLopCN(string malopcn)
        {
            var malopcnParameter = malopcn != null ?
                new ObjectParameter("malopcn", malopcn) :
                new ObjectParameter("malopcn", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteLopCN", malopcnParameter);
        }
    
        public virtual int DeleteMon(string mamon)
        {
            var mamonParameter = mamon != null ?
                new ObjectParameter("mamon", mamon) :
                new ObjectParameter("mamon", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteMon", mamonParameter);
        }
    
        public virtual int UpdateDiemCK(string masv, string malop, Nullable<double> diemck)
        {
            var masvParameter = masv != null ?
                new ObjectParameter("masv", masv) :
                new ObjectParameter("masv", typeof(string));
    
            var malopParameter = malop != null ?
                new ObjectParameter("malop", malop) :
                new ObjectParameter("malop", typeof(string));
    
            var diemckParameter = diemck.HasValue ?
                new ObjectParameter("diemck", diemck) :
                new ObjectParameter("diemck", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateDiemCK", masvParameter, malopParameter, diemckParameter);
        }
    
        public virtual int UpdateDiemGK(string masv, string malop, Nullable<double> diemgk)
        {
            var masvParameter = masv != null ?
                new ObjectParameter("masv", masv) :
                new ObjectParameter("masv", typeof(string));
    
            var malopParameter = malop != null ?
                new ObjectParameter("malop", malop) :
                new ObjectParameter("malop", typeof(string));
    
            var diemgkParameter = diemgk.HasValue ?
                new ObjectParameter("diemgk", diemgk) :
                new ObjectParameter("diemgk", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateDiemGK", masvParameter, malopParameter, diemgkParameter);
        }
    
        public virtual int UpdateDKHP(string madkhp, Nullable<System.DateTime> thoigianbd, Nullable<System.DateTime> thoigiankt)
        {
            var madkhpParameter = madkhp != null ?
                new ObjectParameter("madkhp", madkhp) :
                new ObjectParameter("madkhp", typeof(string));
    
            var thoigianbdParameter = thoigianbd.HasValue ?
                new ObjectParameter("thoigianbd", thoigianbd) :
                new ObjectParameter("thoigianbd", typeof(System.DateTime));
    
            var thoigianktParameter = thoigiankt.HasValue ?
                new ObjectParameter("thoigiankt", thoigiankt) :
                new ObjectParameter("thoigiankt", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateDKHP", madkhpParameter, thoigianbdParameter, thoigianktParameter);
        }
    
        public virtual int UpdateLopCN(string malopcn, string magv)
        {
            var malopcnParameter = malopcn != null ?
                new ObjectParameter("malopcn", malopcn) :
                new ObjectParameter("malopcn", typeof(string));
    
            var magvParameter = magv != null ?
                new ObjectParameter("magv", magv) :
                new ObjectParameter("magv", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateLopCN", malopcnParameter, magvParameter);
        }
    
        public virtual int UpdateMon(string mamon, string tenmon, Nullable<int> sotiet, Nullable<int> sotc, Nullable<double> hsck)
        {
            var mamonParameter = mamon != null ?
                new ObjectParameter("mamon", mamon) :
                new ObjectParameter("mamon", typeof(string));
    
            var tenmonParameter = tenmon != null ?
                new ObjectParameter("tenmon", tenmon) :
                new ObjectParameter("tenmon", typeof(string));
    
            var sotietParameter = sotiet.HasValue ?
                new ObjectParameter("sotiet", sotiet) :
                new ObjectParameter("sotiet", typeof(int));
    
            var sotcParameter = sotc.HasValue ?
                new ObjectParameter("sotc", sotc) :
                new ObjectParameter("sotc", typeof(int));
    
            var hsckParameter = hsck.HasValue ?
                new ObjectParameter("hsck", hsck) :
                new ObjectParameter("hsck", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateMon", mamonParameter, tenmonParameter, sotietParameter, sotcParameter, hsckParameter);
        }
    
        public virtual int UpdateThongTinGV(string magv, string hoten, string gioitinh, Nullable<System.DateTime> ngaysinh, string sdt, string diachi, string quequan, string capbac, string email, Nullable<System.DateTime> ngayvaolam)
        {
            var magvParameter = magv != null ?
                new ObjectParameter("magv", magv) :
                new ObjectParameter("magv", typeof(string));
    
            var hotenParameter = hoten != null ?
                new ObjectParameter("hoten", hoten) :
                new ObjectParameter("hoten", typeof(string));
    
            var gioitinhParameter = gioitinh != null ?
                new ObjectParameter("gioitinh", gioitinh) :
                new ObjectParameter("gioitinh", typeof(string));
    
            var ngaysinhParameter = ngaysinh.HasValue ?
                new ObjectParameter("ngaysinh", ngaysinh) :
                new ObjectParameter("ngaysinh", typeof(System.DateTime));
    
            var sdtParameter = sdt != null ?
                new ObjectParameter("sdt", sdt) :
                new ObjectParameter("sdt", typeof(string));
    
            var diachiParameter = diachi != null ?
                new ObjectParameter("diachi", diachi) :
                new ObjectParameter("diachi", typeof(string));
    
            var quequanParameter = quequan != null ?
                new ObjectParameter("quequan", quequan) :
                new ObjectParameter("quequan", typeof(string));
    
            var capbacParameter = capbac != null ?
                new ObjectParameter("capbac", capbac) :
                new ObjectParameter("capbac", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var ngayvaolamParameter = ngayvaolam.HasValue ?
                new ObjectParameter("ngayvaolam", ngayvaolam) :
                new ObjectParameter("ngayvaolam", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateThongTinGV", magvParameter, hotenParameter, gioitinhParameter, ngaysinhParameter, sdtParameter, diachiParameter, quequanParameter, capbacParameter, emailParameter, ngayvaolamParameter);
        }
    
        public virtual int UpdateThongTinSV(string masv, string hoten, string gioitinh, Nullable<System.DateTime> ngaysinh, string sdt, string diachi, string quequan, string bachoc)
        {
            var masvParameter = masv != null ?
                new ObjectParameter("masv", masv) :
                new ObjectParameter("masv", typeof(string));
    
            var hotenParameter = hoten != null ?
                new ObjectParameter("hoten", hoten) :
                new ObjectParameter("hoten", typeof(string));
    
            var gioitinhParameter = gioitinh != null ?
                new ObjectParameter("gioitinh", gioitinh) :
                new ObjectParameter("gioitinh", typeof(string));
    
            var ngaysinhParameter = ngaysinh.HasValue ?
                new ObjectParameter("ngaysinh", ngaysinh) :
                new ObjectParameter("ngaysinh", typeof(System.DateTime));
    
            var sdtParameter = sdt != null ?
                new ObjectParameter("sdt", sdt) :
                new ObjectParameter("sdt", typeof(string));
    
            var diachiParameter = diachi != null ?
                new ObjectParameter("diachi", diachi) :
                new ObjectParameter("diachi", typeof(string));
    
            var quequanParameter = quequan != null ?
                new ObjectParameter("quequan", quequan) :
                new ObjectParameter("quequan", typeof(string));
    
            var bachocParameter = bachoc != null ?
                new ObjectParameter("bachoc", bachoc) :
                new ObjectParameter("bachoc", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateThongTinSV", masvParameter, hotenParameter, gioitinhParameter, ngaysinhParameter, sdtParameter, diachiParameter, quequanParameter, bachocParameter);
        }
    }
}
