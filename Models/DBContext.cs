using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;

namespace ProjectDAA1.Models
{
    public class DBContext
    {
        public List<BangDiem> dsbd { set; get; }
        public List<dangkyhocphan> dsdkhp { set; get; }
        public List<hoc> dshoc { set; get; }
        public List<mon> dsmon { set; get; }
        public List<lop> dslopdahoc { set; get; }
        public List<lop> dslopchuahoc { set; get; }
    }
}