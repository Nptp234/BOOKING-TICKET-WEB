using CNPMNC_REPORT1.Factory.FactoryPC;
using CNPMNC_REPORT1.Models.PhongChieuPhim;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Singleton
{
    public class SingletonPC : RoomFactory
    {
        private static SingletonPC instance;
        private static readonly object lockObject = new object();

        private SingletonPC()
        {
            SQLConnection db = SQLConnection.Instance;
            string query = "SELECT * FROM PHONGCHIEU;";
            db.OpenConnection();
            allPC = db.Select<PhongChieu>(query);
            db.CloseConnection();
        }

        public static SingletonPC Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SingletonPC();
                    }
                    return instance;
                }
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<PhongChieu> CreatePC()
        {
            return allPC;
        }
    }
}