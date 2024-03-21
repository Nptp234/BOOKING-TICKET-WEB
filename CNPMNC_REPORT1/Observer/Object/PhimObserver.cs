using CNPMNC_REPORT1.Factory;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer
{
    public class PhimObserver : IObserver
    {
        SQLPhim sqlPhim;
        public override void PerformAction(object obj, ActionType actionType)
        {
            if (obj is Phim)
            {
                Phim phim = (Phim)obj;
                sqlPhim = new SQLPhim();

                switch (actionType)
                {
                    case ActionType.Add: Add(phim); break;

                    case ActionType.Update: Update(phim); break;

                    case ActionType.Remove: Remove(phim); break;

                    default: break;
                }
                
            }
        }

        private void Add(Phim phim)
        {
            bool isAdd = sqlPhim.ThemPhim(phim);

            if (isAdd)
            {
                phim.MaPhim = sqlPhim.GetMaxIDFilm();
                PhimFactory.allPhim.Add(phim);
            }
        }

        private void Update(Phim phim)
        {
            bool isUpdate = sqlPhim.CapNhatP(phim);

            if (isUpdate)
            {
                int index = PhimFactory.allPhim.FindIndex(p => p.MaPhim == phim.MaPhim);

                if (index != -1)
                {
                    PhimFactory.allPhim.RemoveAt(index);

                    PhimFactory.allPhim.Insert(index, phim);
                }
            }
        }
    
        private void Remove(Phim phim)
        {
            bool isRemove = sqlPhim.XoaPhim(phim.MaPhim);

            if (isRemove)
            {
                int index = PhimFactory.allPhim.FindIndex(p => p.MaPhim == phim.MaPhim);
                PhimFactory.allPhim.RemoveAt(index);
            }
        }
    }
}