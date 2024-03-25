using CNPMNC_REPORT1.Factory.FactoryLNV;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Singleton
{
    public class SingletonLNV : LNVFactory
    {
        private static SingletonLNV instance;
        private SQLLoaiNV sQLLoaiNV;

        private SingletonLNV()
        {
            sQLLoaiNV = new SQLLoaiNV();
            allLNV = sQLLoaiNV.GetList();
            //SQLConnection db = SQLConnection.Instance;
            //string query = "SELECT * FROM LOAITKNV;";
            //db.OpenConnection();
            //allLNV = db.Select<LoaiNV>(query);
            //db.CloseConnection();
        }

        public static SingletonLNV Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonLNV();
                }
                return instance;
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<LoaiNV> CreateLNV()
        {
            return allLNV;
        }
    }
}