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
    
    public partial class taikhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public taikhoan()
        {
            this.thongbaos = new HashSet<thongbao>();
        }
    
        public string matk { get; set; }
        public string password { get; set; }
        public string masv { get; set; }
        public string magv { get; set; }
        public string nhom { get; set; }
    
        public virtual giangvien giangvien { get; set; }
        public virtual sinhvien sinhvien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<thongbao> thongbaos { get; set; }
    }
}
