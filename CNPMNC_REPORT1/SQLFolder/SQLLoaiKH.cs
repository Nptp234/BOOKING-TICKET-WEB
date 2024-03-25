using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLFolder
{
    public class SQLLoaiKH : SQLObject
    {
        public SQLLoaiKH()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public List<LoaiKH> GetList()
        {
            return LayDS<LoaiKH>("SELECT * FROM LOAIKH;");
        }

        public bool ThemLoaiKH(LoaiKH lkh)
        {
            string query = $"INSERT INTO LOAIKH VALUES(N'{lkh.TenLKH}', '{lkh.ChietKhau}')";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatLKH(LoaiKH lkh)
        {
            string query = $"UPDATE LOAIKH SET TenLKH = N'{lkh.TenLKH}', ChietKhau = '{lkh.ChietKhau}' WHERE MaLoaiKH = '{lkh.MaLoaiKH}'";
            return ThucHienTruyVan(query);
        }

        public bool XoaLKH(string MaLKH)
        {
            string query = $"DELETE FROM LOAIKH WHERE MaLKH = '{MaLKH}'";
            return ThucHienTruyVan(query);
        }
    }
}