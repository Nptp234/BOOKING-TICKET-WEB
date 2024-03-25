using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryBL
{
    public class SingletonBL : BinhLuanFactory
    {
        private static SingletonBL instance;
        private SQLBinhLuan sQLBinhLuan;

        private SingletonBL()
        {
            sQLBinhLuan = new SQLBinhLuan();
            allBL = sQLBinhLuan.GetList();
            //SQLConnection db = SQLConnection.Instance;
            //string query = "SELECT * FROM BINHLUAN;";
            //db.OpenConnection();
            //allBL = db.Select<BinhLuan>(query);
            //db.CloseConnection();
        }

        public static SingletonBL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonBL();
                }
                return instance;
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<BinhLuan> CreateBL()
        {
            return allBL;
        }
    }
}