using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLObject
    {
        protected SQLConnection sQLConnection;

        public SQLObject()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public bool ThucHienTruyVan(string query)
        {
            sQLConnection.OpenConnection();
            int result = sQLConnection.ExecuteNonQuery(query);
            sQLConnection.CloseConnection();

            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public List<T> LayDS<T>(string sql) where T : new()
        {
            List<T> ds = new List<T>();

            sQLConnection.OpenConnection();
            ds = sQLConnection.Select<T>(sql);
            sQLConnection.CloseConnection();

            return ds;
        }
    }
}