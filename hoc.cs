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
    
    public partial class hoc
    {
        public int idhoc { get; set; }
        public Nullable<int> idlop { get; set; }
        public Nullable<int> idsv { get; set; }
        public Nullable<double> diemqt { get; set; }
        public Nullable<double> diemth { get; set; }
        public Nullable<double> diemgk { get; set; }
        public Nullable<double> diemck { get; set; }
        public Nullable<double> diemtb { get; set; }
        public virtual lop lop { get; set; }
        public virtual sinhvien sinhvien { get; set; }
    }
}
