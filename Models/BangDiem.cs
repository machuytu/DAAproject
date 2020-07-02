using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDAA1.Models
{
    public class BangDiem
    {
        public string hoten { get; set; }
        public string masv { get; set; }
        public Nullable<int> idsv { get; set; }
        public Nullable<double> diemqt { get; set; }
        public Nullable<double> diemth { get; set; }
        public Nullable<double> diemgk { get; set; }
        public Nullable<double> diemck { get; set; }
        public Nullable<double> diemtb { get; set; }
    }
}