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
    
    public partial class khoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public khoa()
        {
            this.lops = new HashSet<lop>();
            this.lopcns = new HashSet<lopcn>();
            this.giangviens = new HashSet<giangvien>();
        }
    
        public int idkhoa { get; set; }
        public string tenkhoa { get; set; }
        public string makhoa { get; set; }
        public Nullable<int> idgv { get; set; }
        public string tenvama { get { return tenkhoa + "-" + makhoa; } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lop> lops { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lopcn> lopcns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<giangvien> giangviens { get; set; }
        public virtual giangvien giangvien { get; set; }
    }
}
