using CNPMNC_REPORT1.Factory.FactoryTLVP;
using CNPMNC_REPORT1.Models.Film;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Singleton
{
    public class SingletonTLVP : TLVPFactory
    {
        private static SingletonTLVP instance;
        private static readonly object lockObject = new object();

        private SingletonTLVP()
        {
            SQLConnection db = SQLConnection.Instance;
            string query = "SELECT * FROM TL_P;";
            db.OpenConnection();
            allTLVP = db.Select<TheLoaiVaPhim>(query);
            db.CloseConnection();
        }

        public static SingletonTLVP Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SingletonTLVP();
                    }
                    return instance;
                }
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<TheLoaiVaPhim> CreateTLVP()
        {
            return allTLVP;
        }
    }
}