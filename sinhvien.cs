//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectDAA1
{
    using System;
    using System.Collections.Generic;
    
    public partial class sinhvien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sinhvien()
        {
            this.hocs = new HashSet<hoc>();
            this.taikhoans = new HashSet<taikhoan>();
        }
    
        public int idsv { get; set; }
        public string hoten { get; set; }
        public string masv { get; set; }
        public string gioitinh { get; set; }
        public System.DateTime ngaysinh { get; set; }
        public Nullable<int> idlopcn { get; set; }
        public string diachi { get; set; }
        public string quequan { get; set; }
        public string sdt { get; set; }
        public string bachoc { get; set; }
        public string hinhanh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hoc> hocs { get; set; }
        public virtual lopcn lopcn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taikhoan> taikhoans { get; set; }
    }
}
