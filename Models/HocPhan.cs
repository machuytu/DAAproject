using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDAA1.Models
{
    public class HocPhan
    {
        public int idlop { get; set; }
        public string malop { get; set; }
        public string tenmon { get; set; }
        public string mamon { get; set; }
        public string tengv { get; set; }
        public Nullable<int> tclt { get; set; }
        public Nullable<int> tcth { get; set; }
        public int thu { get; set; }
        public int tietbd { get; set; }
        public Nullable<int> tietkt { get; set; }
        public string strca
        {
            get
            {
                var str = "thứ " + thu.ToString() + " tiết ";
                for (var i = tietbd; i <= tietkt; i++)
                {
                    var x = i;
                    if (i == 10)
                    {
                        x = 0;
                    }
                    str += x.ToString();
                }
                return str;
            }
        }
        public string strtinchi
        {
            get{ return "lý thuyết: " + tclt.ToString() + " / thực hành: " + tcth.ToString(); }
        }

    }
}