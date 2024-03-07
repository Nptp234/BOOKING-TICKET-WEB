using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.VePhim;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLFolder
{
    public class SQLVePhim : SQLObject
    {
        public SQLVePhim()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public bool XoaVePhim(string maVe)
        {
            string query = $"DELETE FROM VEPHIM WHERE MaVe = '{maVe}';";
            return ThucHienTruyVan(query);
        }

        public bool XoaVeGhe(string maVe)
        {
            string query = $"DELETE FROM VE_GHE WHERE MaVe = '{maVe}';";
            return ThucHienTruyVan(query);
        }

        public bool KiemTraXoaVe(string maVe)
        {
            string query = $"SELECT vp.* FROM VEPHIM vp JOIN CHITIETHD cthd ON cthd.MaVe = vp.MaVe WHERE vp.MaVe = '{maVe}';";
            List<VeP> lsVe = LayDS<VeP>(query);

            if (lsVe.Count==0)
            {
                bool isDel1 = XoaVeGhe(maVe);
                bool isDel2 = XoaVePhim(maVe);
                if (isDel1)
                {
                    return isDel2;
                }
                else return false;
            }
            else return false;
        }

        public List<VeP> LayDanhSachVePhimTuMaKH(string maKH)
        {
            string query = $"SELECT * FROM VEPHIM WHERE MaKH = '{maKH}'";

            List<VeP> lsVP = new List<VeP>();

            lsVP = LayDS<VeP>(query);

            if (lsVP.Count > 0)
            {
                return lsVP;
            }
            else return null;
        }

        public List<VeP> LayVePhimLonNhat()
        {
            string query = $"SELECT TOP(1) * FROM VEPHIM ORDER BY MaVe DESC";

            List<VeP> lsVP = new List<VeP>();

            lsVP = LayDS<VeP>(query);

            if (lsVP.Count > 0)
            {
                return lsVP;
            }
            else return null;
        }

        public List<VeGhe> LayDanhSachGheVePhimTuMaVe(string maVe)
        {
            string query = $"SELECT * FROM VE_GHE WHERE MaVe = '{maVe}'";

            List<VeGhe> lsVG = new List<VeGhe>();

            lsVG = LayDS<VeGhe>(query);

            if (lsVG.Count > 0)
            {
                return lsVG;
            }
            else return null;
        }
    }
}