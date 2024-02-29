using CNPMNC_REPORT1.Factory.FactoryLoaiKH;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer.Object
{
    public class LoaiKHObserver : IObserver
    {
        SQLLoaiKH sqlLKH;

        public LoaiKHObserver()
        {
            sqlLKH = new SQLLoaiKH();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is LoaiKH)
            {
                LoaiKH lkh = (LoaiKH)obj;
                sqlLKH = new SQLLoaiKH();

                switch (actionType)
                {
                    case ActionType.Add: Add(lkh); break;

                    case ActionType.Update: Update(lkh); break;

                    case ActionType.Remove: Remove(lkh); break;

                    default: break;
                }

            }
        }

        private void Add(LoaiKH lkh)
        {
            bool isAdd = sqlLKH.ThemLoaiKH(lkh);

            if (isAdd)
            {
                lkh.MaLoaiKH = (LoaiKHFactory.allLKH.Count() + 1).ToString();
                LoaiKHFactory.allLKH.Add(lkh);
            }
        }

        private void Update(LoaiKH lkh)
        {
            bool isUpdate = sqlLKH.CapNhatLKH(lkh);

            if (isUpdate)
            {
                int index = LoaiKHFactory.allLKH.FindIndex(p => p.MaLoaiKH == lkh.MaLoaiKH);

                if (index != -1)
                {
                    LoaiKHFactory.allLKH.RemoveAt(index);

                    LoaiKHFactory.allLKH.Insert(index, lkh);
                }
            }
        }

        private void Remove(LoaiKH lkh)
        {
            bool isRemove = sqlLKH.XoaLKH(lkh.MaLoaiKH);

            if (isRemove)
            {
                int index = LoaiKHFactory.allLKH.FindIndex(p => p.MaLoaiKH == lkh.MaLoaiKH);
                LoaiKHFactory.allLKH.RemoveAt(index);
            }
        }
    }
}