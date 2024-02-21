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