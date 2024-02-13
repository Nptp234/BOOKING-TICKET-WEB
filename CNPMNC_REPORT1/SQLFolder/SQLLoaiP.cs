using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLLoaiP : SQLObject
    {
        public SQLLoaiP()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public bool ThemLoaiPhim(string TenTL, string MoTaTL)
        {
            string query = $"INSERT INTO THELOAIP VALUES({TenTL}, {MoTaTL})";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatLP(string MaTL, string tenCot, string duLieuMoi)
        {
            string query = $"UPDATE THELOAIP SET {tenCot} = {duLieuMoi} WHERE MaTL = {MaTL}";
            return ThucHienTruyVan(query);
        }

        public bool XoaLPhim(string MaTL)
        {
            string query = $"DELETE FROM THELOAIP WHERE MaTL = {MaTL}";
            return ThucHienTruyVan(query);
        }

    }
}