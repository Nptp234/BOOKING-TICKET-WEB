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
        SingletonBL singletonBL;

        public SQLBinhLuan()
        {
            sQLConnection = SQLConnection.Instance;
            singletonBL = SingletonBL.Instance;
        }

        public bool ThemBL(BinhLuan bl)
        {
            string query = $"INSERT INTO BINHLUAN VALUES({bl.MaPhim}, {bl.TenTK}, {bl.GhiChu}, {bl.TrangThai}, {bl.NgayTao})";
            singletonBL.ResetInstance();
            return ThucHienTruyVan(query);
        }

        public bool XoaBL(string maBL)
        {
            string query = $"DELETE FROM BINHLUAN WHERE MaBL = {maBL}";
            singletonBL.ResetInstance();
            return ThucHienTruyVan(query);
        }
    }
}