using CNPMNC_REPORT1.Factory.FactoryLNV;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer.Object
{
    public class LoaiNVObserver : IObserver
    {
        SQLLoaiNV sqlObject = new SQLLoaiNV();
        List<LoaiNV> obj = new List<LoaiNV>();

        public LoaiNVObserver()
        {
            sqlObject = new SQLLoaiNV();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is LoaiNV)
            {
                LoaiNV lpc = (LoaiNV)obj;

                switch (actionType)
                {
                    case ActionType.Add: Add(lpc); break;

                    case ActionType.Update: Update(lpc); break;

                    case ActionType.Remove: Remove(lpc); break;

                    default: break;
                }
            }
        }

        private void Add(LoaiNV obj)
        {
            bool isAdd = sqlObject.ThemLoaiNV(obj);

            if (isAdd)
            {
                obj.MaLNV = (LNVFactory.allLNV.Count() + 1).ToString();
                LNVFactory.allLNV.Add(obj);
            }
        }

        private void Update(LoaiNV obj)
        {
            bool isUpdate = sqlObject.CapNhatLNV(obj);

            if (isUpdate)
            {
                int index = LNVFactory.allLNV.FindIndex(p => p.MaLNV == obj.MaLNV);

                if (index != -1)
                {
                    LNVFactory.allLNV.RemoveAt(index);

                    LNVFactory.allLNV.Insert(index, obj);
                }
            }
        }

        private void Remove(LoaiNV obj)
        {
            bool isRemove = sqlObject.XoaLNV(obj.MaLNV);

            if (isRemove)
            {
                int index = LNVFactory.allLNV.FindIndex(p => p.MaLNV == obj.MaLNV);
                LNVFactory.allLNV.RemoveAt(index);
            }
        }

        public override void GetData()
        {
            obj = sqlObject.GetList();
            CheckChanges();
        }

        public override void CheckChanges()
        {
            if (!IsListEqual(LNVFactory.allLNV, obj))
            {
                LNVFactory.allLNV = obj;
            }
        }
    }
}