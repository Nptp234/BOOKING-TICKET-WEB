using CNPMNC_REPORT1.Factory.FactoryRoomType;
using CNPMNC_REPORT1.Models.PhongChieuPhim;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer.Object
{
    public class LPCObserver : IObserver
    {
        SQLRoomType sqlObject;

        public LPCObserver()
        {
            sqlObject = new SQLRoomType();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is LoaiPC)
            {
                LoaiPC lpc = (LoaiPC)obj;

                switch (actionType)
                {
                    case ActionType.Add: Add(lpc); break;

                    case ActionType.Update: Update(lpc); break;

                    case ActionType.Remove: Remove(lpc); break;

                    default: break;
                }
            }
        }

        private void Add(LoaiPC obj)
        {
            bool isAdd = sqlObject.ThemLPC(obj);

            if (isAdd)
            {
                obj.MaLPC = (RoomTypeFactory.allLPC.Count() + 1).ToString();
                RoomTypeFactory.allLPC.Add(obj);
            }
        }

        private void Update(LoaiPC obj)
        {
            bool isUpdate = sqlObject.CapNhatLPC(obj);

            if (isUpdate)
            {
                int index = RoomTypeFactory.allLPC.FindIndex(p => p.MaLPC == obj.MaLPC);

                if (index != -1)
                {
                    RoomTypeFactory.allLPC.RemoveAt(index);

                    RoomTypeFactory.allLPC.Insert(index, obj);
                }
            }
        }

        private void Remove(LoaiPC obj)
        {
            bool isRemove = sqlObject.XoaLPC(obj.MaLPC);

            if (isRemove)
            {
                int index = RoomTypeFactory.allLPC.FindIndex(p => p.MaLPC == obj.MaLPC);
                RoomTypeFactory.allLPC.RemoveAt(index);
            }
        }
    }
}