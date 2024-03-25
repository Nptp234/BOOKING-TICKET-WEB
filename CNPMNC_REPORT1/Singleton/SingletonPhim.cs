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
        private SQLPhim sQLPhim;

        private SingletonPhim()
        {
            sQLPhim = new SQLPhim();
            allPhim = sQLPhim.GetList();
            // Logic tạo danh sách phim
            //SQLConnection db = SQLConnection.Instance;
            //string query = "SELECT * FROM PHIM;";
            //db.OpenConnection();
            //allPhim = db.Select<Phim>(query);
            //db.CloseConnection();
        }

        public static SingletonPhim Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonPhim();
                }
                return instance;
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