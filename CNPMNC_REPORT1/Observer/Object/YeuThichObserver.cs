using CNPMNC_REPORT1.Factory.FactoryYT;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer.Object
{
    public class YeuThichObserver : IObserver
    {
        SQLYeuThich sqlObject;

        public YeuThichObserver()
        {
            sqlObject = new SQLYeuThich();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is YeuThich)
            {
                YeuThich yt = (YeuThich)obj;

                switch (actionType)
                {
                    case ActionType.Add: Add(yt); break;

                    case ActionType.Remove: Remove(yt); break;

                    default: break;
                }
            }
        }

        private void Add(YeuThich obj)
        {
            bool isAdd = sqlObject.ThemYT(obj);

            if (isAdd)
            {
                YeuThichFactory.allYT.Add(obj);
            }
        }

        private void Remove(YeuThich obj)
        {
            bool isRemove = sqlObject.XoaYT(obj);

            if (isRemove)
            {
                int index = YeuThichFactory.allYT.FindIndex(p => p.MaPhim == obj.MaPhim);
                YeuThichFactory.allYT.RemoveAt(index);
            }
        }
    }
}