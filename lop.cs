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
    
    public partial class lop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lop()
        {
            this.hocs = new HashSet<hoc>();
        }
    
        public int idlop { get; set; }
        public string malop { get; set; }
        public Nullable<int> idgv { get; set; }
        public Nullable<int> idmon { get; set; }
        public Nullable<int> idkhoa { get; set; }
        public int iddkhp { get; set; }
        public int thu { get; set; }
        public int tietbd { get; set; }
        public Nullable<int> tietkt { get; set; }
    
        public virtual dangkyhocphan dangkyhocphan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hoc> hocs { get; set; }
        public virtual khoa khoa { get; set; }
        public virtual mon mon { get; set; }
        public virtual giangvien giangvien { get; set; }
    }
}
