//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class mon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mon()
        {
            this.lops = new HashSet<lop>();
        }
    
        public string mamon { get; set; }
        public string tenmon { get; set; }
        public double hsgk { get; set; }
        public double hsck { get; set; }
        public int sotiet { get; set; }
        public int sotc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lop> lops { get; set; }
    }
}
