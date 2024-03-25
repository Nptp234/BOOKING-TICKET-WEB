using CNPMNC_REPORT1.Factory.FactoryBL;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer
{
    public class BinhLuanObserver : IObserver
    {
        SQLBinhLuan sqlObject = new SQLBinhLuan();
        List<BinhLuan> bls = new List<BinhLuan>();

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is BinhLuan)
            {
                BinhLuan binhLuan = (BinhLuan)obj;
                sqlObject = new SQLBinhLuan();

                switch (actionType)
                {
                    case ActionType.Add: Add(binhLuan); break;

                    case ActionType.Remove: Remove(binhLuan); break;

                    default: break;
                }

            }
        }

        private void Add(BinhLuan obj)
        {
            bool isAdd = sqlObject.ThemBL(obj);

            if (isAdd)
            {
                obj.MaBL = (BinhLuanFactory.allBL.Count() + 1).ToString();
                BinhLuanFactory.allBL.Add(obj);
            }
        }

        private void Remove(BinhLuan obj)
        {
            bool isRemove = sqlObject.XoaBL(obj.MaBL);

            if (isRemove)
            {
                int index = BinhLuanFactory.allBL.FindIndex(p => p.MaBL == obj.MaBL);
                BinhLuanFactory.allBL.RemoveAt(index);
            }
        }

        public override void CheckChanges()
        {
            if (!IsListEqual(BinhLuanFactory.allBL, bls))
            {
                BinhLuanFactory.allBL = bls;
            }
        }

        public override void GetData()
        {
            bls = sqlObject.GetList();
            CheckChanges();
        }
    }
}