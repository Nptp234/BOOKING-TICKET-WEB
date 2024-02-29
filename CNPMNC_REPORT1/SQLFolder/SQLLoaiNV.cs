using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLFolder
{
    public class SQLLoaiNV : SQLObject
    {
        public SQLLoaiNV()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public bool ThemLoaiNV(LoaiNV lnv)
        {
            string query = $"INSERT INTO LOAITKNV VALUES(N'{lnv.TenLNV}')";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatLNV(LoaiNV lnv)
        {
            string query = $"UPDATE LOAITKNV SET TenLNV = N'{lnv.TenLNV}' WHERE MaLNV = '{lnv.MaLNV}'";
            return ThucHienTruyVan(query);
        }

        public bool XoaLNV(string MaLNV)
        {
            string query = $"DELETE FROM LOAITKNV WHERE MaLNV = '{MaLNV}'";
            return ThucHienTruyVan(query);
        }

        public bool KiemTraTenLNV(string TenLNV)
        {
            bool isCheck = false;

            string query = $"SELECT * FROM LOAITKNV WHERE TenLNV = N'{TenLNV}'";
            List<LoaiNV> ls = LayDS<LoaiNV>(query);

            if (ls.Count >= 1)
            {
                isCheck = false;
            }
            else isCheck = true;

            return isCheck;
        }
    }
}