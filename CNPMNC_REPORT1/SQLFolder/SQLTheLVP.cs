using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.Film;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLFolder
{
    public class SQLTheLVP : SQLObject
    {
        public SQLTheLVP()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public List<TheLoaiVaPhim> GetList()
        {
            return LayDS<TheLoaiVaPhim>("SELECT * FROM TL_P;");
        }

        public bool ThemTLVP(TheLoaiVaPhim lPhim)
        {
            string query = $"INSERT INTO TL_P VALUES('{lPhim.MaPhim}', '{lPhim.MaTL}')";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatTLVP(TheLoaiVaPhim lPhim)
        {
            string query = $"UPDATE TL_P SET MaPhim = '{lPhim.MaPhim}', MaTL = N'{lPhim.MaTL}' WHERE MaTLP = '{lPhim.MaTLP}'";
            return ThucHienTruyVan(query);
        }

        public bool XoaTLVPhim(string MaTLVP)
        {
            string query = $"DELETE FROM TL_P WHERE MaTLP = '{MaTLVP}'";
            return ThucHienTruyVan(query);
        }

        public List<TheLoaiVaPhim> LayDSMaPhimTheoMaTL(string matl)
        {
            string query = $"SELECT * FROM TL_P WHERE MaTL = '{matl}'";

            List<TheLoaiVaPhim> lst = LayDS<TheLoaiVaPhim>(query);

            return lst;
        }

        public bool KiemTraTrungPhimTheLoai(string maPhim, string maTL)
        {
            bool isCheck = false;

            string query = $"SELECT * FROM TL_P WHERE MaPhim = '{maPhim}' AND MaTL = '{maTL}';";

            List<TheLoaiVaPhim> countLS = LayDS<TheLoaiVaPhim>(query);

            if (countLS.Count >= 1)
            {
                isCheck = false;
            }
            else isCheck = true;

            return isCheck;
        }
    }
}