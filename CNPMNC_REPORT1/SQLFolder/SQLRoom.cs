using CNPMNC_REPORT1.Models.PhongChieuPhim;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLFolder
{
    public class SQLRoom : SQLObject
    {
        public SQLRoom()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public bool ThemPC(PhongChieu room)
        {
            string query = $"INSERT INTO PHONGCHIEU VALUES(N'{room.TenPC}', '{room.SLGheThuong}', '{room.SLGheVIP}', '{room.MaLPC}')";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatPC(PhongChieu room)
        {
            string query = $"UPDATE PHONGCHIEU SET TenPC = N'{room.TenPC}', SLGheThuong = '{room.SLGheThuong}', SLGheVIP = '{room.SLGheVIP}', MaLPC = '{room.MaLPC}' WHERE MaPC = '{room.MaPC}'";
            return ThucHienTruyVan(query);
        }

        public bool XoaPC(string MaPC)
        {
            string query = $"DELETE FROM PHONGCHIEU WHERE MaPC = '{MaPC}'";
            return ThucHienTruyVan(query);
        }
    }
}