using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public bool ThemLoaiPhim(LoaiPhim lPhim)
        {
            string query = $"INSERT INTO THELOAIP VALUES(N'{lPhim.TenTL}', N'{lPhim.MoTaTL}')";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatLP(LoaiPhim lPhim)
        {
            string query = $"UPDATE THELOAIP SET TenTL = N'{lPhim.TenTL}', MoTaTL = N'{lPhim.MoTaTL}' WHERE MaTL = '{lPhim.MaTL}'";
            return ThucHienTruyVan(query);
        }

        public bool XoaLPhim(string MaTL)
        {
            string query = $"DELETE FROM THELOAIP WHERE MaTL = '{MaTL}'";
            return ThucHienTruyVan(query);
        }

        public string ChuyenTen_Ma(string TenLP)
        {
            string MaLPC = "";
            string query = $"SELECT MaLPC FROM LOAIPC WHERE TenLPC = N'{TenLP}'";

            SQLConnection connection = SQLConnection.Instance;

            SqlCommand command = new SqlCommand(query, connection.GetConnection());

            connection.OpenConnection();

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                if (sqlDataReader.Read())
                {
                    MaLPC = sqlDataReader.GetInt32(0).ToString();
                }
            }
            else MaLPC = "";

            connection.CloseConnection();

            return MaLPC;
        }

    }
}