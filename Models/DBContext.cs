using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ProjectDAA1.Models
{
    public class DBContext
    {
        //public List<KQHT> dskqht { set; get; }
        public List<hoc> dskqht { set; get; }
        public List<dangkyhocphan> dsdkhp { set; get; }
        public List<lop> dslop { set; get; }
        public List<lop> dslopdadk { set; get; }
        public List<lop> dslopchuadk { set; get; }
        public List<lop> dslopdadkfull { set; get; }
        public List<lop> dslopchuadkfull { set; get; }
    }
}