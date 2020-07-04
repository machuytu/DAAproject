﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ProjectDAA1.Models
{
    public class DBContext
    {
        public int id { get; set; }
        public mon mon { set; get; }
        public List<KQHT> dskqht { set; get; }
        public List<BangDiem> dsbd { set; get; }
        public List<dangkyhocphan> dsdkhp { set; get; }
        public List<HocPhan> dslopdahoc { set; get; }
        public List<HocPhan> dslopchuahoc { set; get; }
    }
}