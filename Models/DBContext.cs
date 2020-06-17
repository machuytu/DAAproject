using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDAA1.Models
{
    public class DBContext
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        public string malopcn (int idlopcn) 
        {
            return db.lopcns.Where(x => x.idlopcn == idlopcn).SingleOrDefault()?.malopcn.ToString();
        }
        public string malop (int idlop)
        {
            return db.lops.Where(x => x.idlop == idlop).SingleOrDefault()?.malop.ToString();
        }
        public List<lop> gv_lop (int idgv)
        {
            return db.lops.Where(x => x.idgv == idgv).ToList();
        }
        public List<lop> gv_lopcn(int idgv)
        {
            return db.lops.Where(x => x.idgv == idgv).ToList();
        }
        public List<sinhvien> lop_sv (int idlop)
        {
            return (from sv in db.sinhviens join h in db.hocs on sv.idsv equals h.idsv 
                    where h.idlop == idlop select sv).ToList();
        }
        public List<sinhvien> lopcn_sv(int idlopcn)
        {
            return db.sinhviens.Where(x => x.idlopcn == idlopcn).ToList();
        }
    }
}