using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDAA1.Models
{
    public class BangDiem
    {
        public int iddkhp { get; set; }
        public string tenmon { get; set; }
        public string mamon { get; set; }
        public Nullable<int> sotc { get; set; }
        public Nullable<double> hsqt { get; set; }
        public Nullable<double> hsth { get; set; }
        public Nullable<double> hsgk { get; set; }
        public Nullable<double> hsck { get; set; }
        public Nullable<double> diemqt { get; set; }
        public Nullable<double> diemth { get; set; }
        public Nullable<double> diemgk { get; set; }
        public Nullable<double> diemck { get; set; }
        public Nullable<double> diemtb { get; set; }
    }
}