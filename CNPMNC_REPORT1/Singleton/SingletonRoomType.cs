using CNPMNC_REPORT1.Factory.FactoryRoomType;
using CNPMNC_REPORT1.Models.PhongChieuPhim;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Singleton
{
    public class SingletonRoomType : RoomTypeFactory
    {
        private static SingletonRoomType instance;
        private SQLRoomType sQLRoomType;

        private SingletonRoomType()
        {
            sQLRoomType = new SQLRoomType();
            allLPC = sQLRoomType.GetList();
            //SQLConnection db = SQLConnection.Instance;
            //string query = "SELECT * FROM LOAIPC;";
            //db.OpenConnection();
            //allLPC = db.Select<LoaiPC>(query);
            //db.CloseConnection();
        }

        public static SingletonRoomType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonRoomType();
                }
                return instance;
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public override List<LoaiPC> CreateLPC()
        {
            return allLPC;
        }
    }
}