using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLGioiHanTuoi : SQLObject
    {
        public SQLGioiHanTuoi()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public bool ThemGHT(string TenGHT, string MoTaGHT)
        {
            string query = $"INSERT INTO GIOIHANTUOI VALUES({TenGHT}, {MoTaGHT})";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatGHT(string MaGHT, string tenCot, string duLieuMoi)
        {
            string query = $"UPDATE GIOIHANTUOI SET {tenCot} = {duLieuMoi} WHERE MaTL = {MaGHT}";
            return ThucHienTruyVan(query);
        }

        public bool XoaGHT(string MaGHT)
        {
            string query = $"DELETE FROM GIOIHANTUOI WHERE MaGHT = {MaGHT}";
            return ThucHienTruyVan(query);
        }

        public string ChuyenTen_Ma(string TenGHT)
        {
            string MaGHT = "";
            string query = $"SELECT MaGHT FROM GIOIHANTUOI WHERE TenGHT = {TenGHT}";

            SQLConnection connection = SQLConnection.Instance;

            SqlCommand command = new SqlCommand(query, connection.GetConnection());

            connection.OpenConnection();

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                if (sqlDataReader.Read())
                {
                    MaGHT = sqlDataReader.GetString(0);
                }
            }
            else MaGHT = "";

            connection.CloseConnection();

            return MaGHT;
        }

    }
}