﻿using CNPMNC_REPORT1.Factory.FactoryLoaiPhim;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer
{
    public class LoaiPhimObserver : IObserver
    {
        SQLLoaiP sqlLP = new SQLLoaiP();
        List<LoaiPhim> obj = new List<LoaiPhim>();

        public LoaiPhimObserver()
        {
            sqlLP = new SQLLoaiP();
        }

        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is LoaiPhim)
            {
                LoaiPhim lphim = (LoaiPhim)obj;
                sqlLP = new SQLLoaiP();

                switch (actionType)
                {
                    case ActionType.Add: Add(lphim); break;

                    case ActionType.Update: Update(lphim); break;

                    case ActionType.Remove: Remove(lphim); break;

                    default: break;
                }

            }
        }

        private void Add(LoaiPhim lphim)
        {
            bool isAdd = sqlLP.ThemLoaiPhim(lphim);

            if (isAdd)
            {
                lphim.MaTL = (LoaiPhimFactory.allLoaiP.Count() + 1).ToString();
                LoaiPhimFactory.allLoaiP.Add(lphim);
            }
        }

        private void Update(LoaiPhim lphim)
        {
            bool isUpdate = sqlLP.CapNhatLP(lphim);

            if (isUpdate)
            {
                int index = LoaiPhimFactory.allLoaiP.FindIndex(p => p.MaTL == lphim.MaTL);

                if (index != -1)
                {
                    LoaiPhimFactory.allLoaiP.RemoveAt(index);

                    LoaiPhimFactory.allLoaiP.Insert(index, lphim);
                }
            }
        }

        private void Remove(LoaiPhim lphim)
        {
            bool isRemove = sqlLP.XoaLPhim(lphim.MaTL);

            if (isRemove)
            {
                int index = LoaiPhimFactory.allLoaiP.FindIndex(p => p.MaTL == lphim.MaTL);
                LoaiPhimFactory.allLoaiP.RemoveAt(index);
            }
        }

        public override void GetData()
        {
            obj = sqlLP.GetList();
            CheckChanges();
        }

        public override void CheckChanges()
        {
            if (!IsListEqual(LoaiPhimFactory.allLoaiP, obj))
            {
                LoaiPhimFactory.allLoaiP = obj;
            }
        }
    }
}