using CNPMNC_REPORT1.Factory.FactoryYT;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Singleton
{
    public class SingletonYT : YeuThichFactory
    {

        private static SingletonYT instance;
        private static readonly object lockObject = new object();
        private SQLUser sqlUser;

        private SingletonYT()
        {
            SQLConnection db = SQLConnection.Instance;
            sqlUser = SQLUser.Instance;
            string query = $"SELECT * FROM YEUTHICH WHERE MaKH = '{sqlUser.KH.MaKH}';";
            db.OpenConnection();
            allYT = db.Select<YeuThich>(query);
            db.CloseConnection();
        }

        public static SingletonYT Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SingletonYT();
                    }
                    return instance;
                }
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<YeuThich> CreateYT()
        {
            return allYT;
        }
    }
}