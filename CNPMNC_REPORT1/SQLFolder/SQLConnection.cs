using CNPMNC_REPORT1.SQLData;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLConnection
    {
        private static SQLConnection instance;
        private static readonly object lockObject = new object();
        public string connectionString;
        public SqlConnection connection;

        private SQLConnection()
        {
            this.connectionString = "Server=localhost;Database=CNPMNC_DATA1;Trusted_Connection=True";
            this.connection = new SqlConnection(connectionString);
        }

        public static SQLConnection Instance
        {
            get
            {
                // Sử dụng double-check locking để đảm bảo thread-safe
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new SQLConnection();
                        }
                    }
                }
                return instance;
            }
        }

        public void OpenConnection()
        {
            this.connection.Open();
        }

        public void CloseConnection()
        {
            this.connection.Close();
        }

        public string GetConnectionString()
        {
            return this.connectionString;
        }

        public SqlConnection GetConnection()
        {
            return this.connection;
        }

        public List<T> Select<T>(string sql) where T : new()
        {
            List<T> listData = new List<T>();

            SqlCommand command = new SqlCommand(sql, this.connection);

            using (SqlDataReader data = command.ExecuteReader())
            {
                while (data.Read())
                {
                    T row = new T();
                    var properties = typeof(T).GetProperties();

                    for (int i = 0; i < data.FieldCount; i++)
                    {
                        var fieldName = data.GetName(i);
                        var property = properties.FirstOrDefault(p => p.Name == fieldName);

                        if (property != null)
                        {
                            var value = data.GetValue(i);
                            if (value != DBNull.Value)
                            {
                                property.SetValue(row, value.ToString());
                            }
                        }
                    }

                    listData.Add(row);
                }
            }

            return listData;
        }

        public int ExecuteNonQuery(string sql)
        {
            SqlCommand command = new SqlCommand(sql, this.connection);
            return command.ExecuteNonQuery();
        }
    }
}