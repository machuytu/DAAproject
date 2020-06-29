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
        public List<HocPhan> dslopdahoc { set; get; }
        public List<HocPhan> dslopchuahoc { set; get; }
    }
}