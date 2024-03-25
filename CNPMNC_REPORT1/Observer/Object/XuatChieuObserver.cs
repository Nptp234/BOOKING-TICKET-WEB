using CNPMNC_REPORT1.Models.LichChieuPhim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer.Object
{
    public class XuatChieuObserver : IObserver
    {
        public override void CheckChanges()
        {
            throw new NotImplementedException();
        }

        public override void GetData()
        {
            throw new NotImplementedException();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is XuatChieu)
            {
                XuatChieu xc = (XuatChieu)obj;

                switch (actionType)
                {
                    case ActionType.Add: Add(xc); break;
                    case ActionType.Update: Update(xc); break;
                    default: break;
                }
            }
        }

        private void Add(XuatChieu xc)
        {

        }

        private void Update(XuatChieu xc)
        {

        }
    }
}