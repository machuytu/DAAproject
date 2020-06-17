using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDAA1
{
    [Serializable]
    public class UserLogin
    {
        public int idtk { set; get; }
        public string matk { set; get; }
        public string Nhom { set; get; }
        public int? idsv { set; get; }
        public int? idgv { set; get; }
    }
}