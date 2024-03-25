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

        public List<GioiHanTuoi> GetList()
        {
            return LayDS<GioiHanTuoi>("SELECT * FROM GIOIHANTUOI;");
        }

        public bool ThemGHT(GioiHanTuoi ght)
        {
            string query = $"INSERT INTO GIOIHANTUOI VALUES(N'{ght.TenGHT}', N'{ght.MoTaGHT}')";
            return ThucHienTruyVan(query);
        }

        public bool CapNhatGHT(GioiHanTuoi ght)
        {
            string query = $"UPDATE GIOIHANTUOI SET TenGHT = N'{ght.TenGHT}', MoTaGHT = N'{ght.MoTaGHT}' WHERE MaGHT = '{ght.MaGHT}'";
            return ThucHienTruyVan(query);
        }

        public bool XoaGHT(GioiHanTuoi ght)
        {
            string query = $"DELETE FROM GIOIHANTUOI WHERE MaGHT = '{ght.MaGHT}'";
            return ThucHienTruyVan(query);
        }

        public string ChuyenTen_Ma(string TenGHT)
        {
            string MaGHT = "";
            string query = $"SELECT MaGHT FROM GIOIHANTUOI WHERE TenGHT = N'{TenGHT}'";

            SQLConnection connection = SQLConnection.Instance;

            SqlCommand command = new SqlCommand(query, connection.GetConnection());

            connection.OpenConnection();

            SqlDataReader sqlDataReader = command.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                if (sqlDataReader.Read())
                {
                    MaGHT = sqlDataReader.GetInt32(0).ToString();
                }
            }
            else MaGHT = "";

            connection.CloseConnection();

            return MaGHT;
        }

    }
}