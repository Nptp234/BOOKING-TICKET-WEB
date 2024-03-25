using CNPMNC_REPORT1.Factory.FactoryGHT;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer
{
    public class GHTObserver : IObserver
    {
        SQLGioiHanTuoi sqlObject = new SQLGioiHanTuoi();
        List<GioiHanTuoi> obj = new List<GioiHanTuoi>();

        public GHTObserver()
        {
            sqlObject = new SQLGioiHanTuoi();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is GioiHanTuoi)
            {
                GioiHanTuoi ght = (GioiHanTuoi)obj;

                switch (actionType)
                {
                    case ActionType.Add: Add(ght); break;

                    case ActionType.Update: Update(ght); break;

                    case ActionType.Remove: Remove(ght); break;

                    default: break;
                }
            }
        }

        private void Add(GioiHanTuoi obj)
        {
            bool isAdd = sqlObject.ThemGHT(obj);

            if (isAdd)
            {
                obj.MaGHT = (GHTFactory.allGHT.Count() + 1).ToString();
                GHTFactory.allGHT.Add(obj);
            }
        }

        private void Update(GioiHanTuoi ght)
        {
            bool isUpdate = sqlObject.CapNhatGHT(ght);

            if (isUpdate)
            {
                int index = GHTFactory.allGHT.FindIndex(p => p.MaGHT == ght.MaGHT);

                if (index != -1)
                {
                    GHTFactory.allGHT.RemoveAt(index);

                    GHTFactory.allGHT.Insert(index, ght);
                }
            }
        }

        private void Remove(GioiHanTuoi obj)
        {
            bool isRemove = sqlObject.XoaGHT(obj);

            if (isRemove)
            {
                int index = GHTFactory.allGHT.FindIndex(p => p.MaGHT == obj.MaGHT);
                GHTFactory.allGHT.RemoveAt(index);
            }
        }

        public override void GetData()
        {
            obj = sqlObject.GetList();
            CheckChanges();
        }

        public override void CheckChanges()
        {
            if (!IsListEqual(GHTFactory.allGHT, obj))
            {
                GHTFactory.allGHT = obj;
            }
        }
    }
}