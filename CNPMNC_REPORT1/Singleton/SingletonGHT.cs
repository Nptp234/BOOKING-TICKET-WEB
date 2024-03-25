using CNPMNC_REPORT1.Factory.FactoryGHT;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Singleton
{
    public class SingletonGHT : GHTFactory
    {
        private static SingletonGHT instance;
        private SQLGioiHanTuoi sQLGioiHanTuoi;

        private SingletonGHT()
        {
            sQLGioiHanTuoi = new SQLGioiHanTuoi();
            allGHT = sQLGioiHanTuoi.GetList();
            //SQLConnection db = SQLConnection.Instance;
            //string query = "SELECT * FROM GIOIHANTUOI;";
            //db.OpenConnection();
            //allGHT = db.Select<GioiHanTuoi>(query);
            //db.CloseConnection();
        }

        public static SingletonGHT Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonGHT();
                }
                return instance;
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<GioiHanTuoi> CreateGHT()
        {
            return allGHT;
        }
    }
}