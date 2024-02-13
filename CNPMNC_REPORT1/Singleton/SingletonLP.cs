using CNPMNC_REPORT1.Factory.FactoryLoaiPhim;
using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Singleton
{
    public class SingletonLP : LoaiPhimFactory
    {
        private static SingletonLP instance;
        private static readonly object lockObject = new object();

        private SingletonLP()
        {
            // Logic tạo danh sách phim
            SQLConnection db = SQLConnection.Instance;
            string query = "SELECT * FROM THELOAIP;";
            db.OpenConnection();
            allLoaiP = db.Select<LoaiPhim>(query);
            db.CloseConnection();
        }

        public static SingletonLP Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SingletonLP();
                    }
                    return instance;
                }
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }
        public override List<LoaiPhim> CreateLoaiP()
        {
            return allLoaiP;
        }
    }
}