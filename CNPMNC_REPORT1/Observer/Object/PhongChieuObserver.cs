using CNPMNC_REPORT1.Factory.FactoryPC;
using CNPMNC_REPORT1.Models.PhongChieuPhim;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer
{
    public class PhongChieuObserver : IObserver
    {
        SQLRoom sqlObject;

        public PhongChieuObserver()
        {
            sqlObject = new SQLRoom();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is PhongChieu)
            {
                PhongChieu pc = (PhongChieu)obj;

                switch (actionType)
                {
                    case ActionType.Add: Add(pc); break;

                    case ActionType.Update: Update(pc); break;

                    case ActionType.Remove: Remove(pc); break;

                    default: break;
                }
            }
        }

        private void Add(PhongChieu obj)
        {
            bool isAdd = sqlObject.ThemPC(obj);

            if (isAdd)
            {
                obj.MaPC = (RoomFactory.allPC.Count() + 1).ToString();
                RoomFactory.allPC.Add(obj);
            }
        }

        private void Update(PhongChieu obj)
        {
            bool isUpdate = sqlObject.CapNhatPC(obj);

            if (isUpdate)
            {
                int index = RoomFactory.allPC.FindIndex(p => p.MaPC == obj.MaPC);

                if (index != -1)
                {
                    RoomFactory.allPC.RemoveAt(index);

                    RoomFactory.allPC.Insert(index, obj);
                }
            }
        }

        private void Remove(PhongChieu obj)
        {
            bool isRemove = sqlObject.XoaPC(obj.MaPC);

            if (isRemove)
            {
                int index = RoomFactory.allPC.FindIndex(p => p.MaPC == obj.MaPC);
                RoomFactory.allPC.RemoveAt(index);
            }
        }
    }
}