using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory
{
    public class SingletonPhim : PhimFactory
    {
        private static SingletonPhim instance;
        private static readonly object lockObject = new object();

        private SingletonPhim()
        {
            // Logic tạo danh sách phim
            SQLConnection db = SQLConnection.Instance;
            string query = "SELECT * FROM PHIM;";
            db.OpenConnection();
            allPhim = db.Select<Phim>(query);
            db.CloseConnection();
        }

        public static SingletonPhim Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SingletonPhim();
                    }
                    return instance;
                }
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<Phim> CreatePhim()
        {
            return allPhim;
        }
    }
}