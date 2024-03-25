using CNPMNC_REPORT1.Factory.FactoryBL;
using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLBinhLuan : SQLObject
    {
        public SQLBinhLuan()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public List<BinhLuan> GetList()
        {
            return LayDS<BinhLuan>("SELECT * FROM BINHLUAN;");
        }

        public bool ThemBL(BinhLuan bl)
        {
            string query = $"INSERT INTO BINHLUAN VALUES('{bl.MaPhim}', '{bl.TenTK}', N'{bl.GhiChu}', '{bl.TrangThai}', '{bl.NgayTao}')";

            return ThucHienTruyVan(query);
        }

        public bool XoaBL(string maBL)
        {
            string query = $"DELETE FROM BINHLUAN WHERE MaBL = {maBL}";

            return ThucHienTruyVan(query);
        }
    }
}