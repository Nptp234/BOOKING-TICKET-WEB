using CNPMNC_REPORT1.Factory.FactoryLoaiKH;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Singleton
{
    public class SingletonLKH : LoaiKHFactory
    {
        private static SingletonLKH instance;
        private static readonly object lockObject = new object();

        private SingletonLKH()
        {
            SQLConnection db = SQLConnection.Instance;
            string query = "SELECT * FROM LOAIKH;";
            db.OpenConnection();
            allLKH = db.Select<LoaiKH>(query);
            db.CloseConnection();
        }

        public static SingletonLKH Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SingletonLKH();
                    }
                    return instance;
                }
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<LoaiKH> CreateLoaiKH()
        {
            return allLKH;
        }
    }
}