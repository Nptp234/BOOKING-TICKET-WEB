using CNPMNC_REPORT1.Models.PhongChieuPhim;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLFolder
{
    public class SQLRoomType : SQLObject
    {
        public SQLRoomType()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public bool ThemLPC(LoaiPC lpc)
        {
            string query = $"INSERT INTO LOAIPC VALUES(N'{lpc.TenLPC}', N'{lpc.MoTaLPC}')";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatLPC(LoaiPC lpc)
        {
            string query = $"UPDATE LOAIPC SET TenLPC = N'{lpc.TenLPC}', MoTaLPC = N'{lpc.MoTaLPC}' WHERE MaLPC = '{lpc.MaLPC}'";
            return ThucHienTruyVan(query);
        }

        public bool XoaLPC(string MaLPC)
        {
            string query = $"DELETE FROM LOAIPC WHERE MaLPC = '{MaLPC}'";
            return ThucHienTruyVan(query);
        }
    }
}