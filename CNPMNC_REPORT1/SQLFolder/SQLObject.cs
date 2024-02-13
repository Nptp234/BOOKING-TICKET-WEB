using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public abstract class SQLObject
    {
        protected SQLConnection sQLConnection;
        protected bool ThucHienTruyVan(string query)
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
    }
}