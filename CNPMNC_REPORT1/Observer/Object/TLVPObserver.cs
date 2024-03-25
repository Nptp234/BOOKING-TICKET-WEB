using CNPMNC_REPORT1.Factory.FactoryTLVP;
using CNPMNC_REPORT1.Models.Film;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer.Object
{
    public class TLVPObserver : IObserver
    {
        SQLTheLVP sqlObject = new SQLTheLVP();
        List<TheLoaiVaPhim> obj = new List<TheLoaiVaPhim>();

        public TLVPObserver()
        {
            sqlObject = new SQLTheLVP();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is TheLoaiVaPhim)
            {
                TheLoaiVaPhim tlvp = (TheLoaiVaPhim)obj;

                switch (actionType)
                {
                    case ActionType.Add: Add(tlvp); break;

                    case ActionType.Update: Update(tlvp); break;

                    case ActionType.Remove: Remove(tlvp); break;

                    default: break;
                }
            }
        }

        private void Add(TheLoaiVaPhim obj)
        {
            bool isAdd = sqlObject.ThemTLVP(obj);

            if (isAdd)
            {
                obj.MaTLP = (TLVPFactory.allTLVP.Count() + 1).ToString();
                TLVPFactory.allTLVP.Add(obj);
            }
        }

        private void Update(TheLoaiVaPhim obj)
        {
            bool isUpdate = sqlObject.CapNhatTLVP(obj);

            if (isUpdate)
            {
                int index = TLVPFactory.allTLVP.FindIndex(p => p.MaTLP == obj.MaTLP);

                if (index != -1)
                {
                    TLVPFactory.allTLVP.RemoveAt(index);

                    TLVPFactory.allTLVP.Insert(index, obj);
                }
            }
        }

        private void Remove(TheLoaiVaPhim obj)
        {
            bool isRemove = sqlObject.XoaTLVPhim(obj.MaTLP);

            if (isRemove)
            {
                int index = TLVPFactory.allTLVP.FindIndex(p => p.MaTLP == obj.MaTLP);
                TLVPFactory.allTLVP.RemoveAt(index);
            }
        }

        public override void GetData()
        {
            obj = sqlObject.GetList();
            CheckChanges();
        }

        public override void CheckChanges()
        {
            if (!IsListEqual(TLVPFactory.allTLVP, obj))
            {
                TLVPFactory.allTLVP = obj;
            }
        }
    }
}