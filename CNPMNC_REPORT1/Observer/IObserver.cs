using CNPMNC_REPORT1.SQLData;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer
{
    public abstract class IObserver
    {
        public abstract void PerformAction(object obj, ActionType actionType);
        public abstract void GetData();
        public abstract void CheckChanges();

        protected bool IsListEqual<T>(List<T> oldLst, List<T> newLst) where T : new()
        {
            if (oldLst == null || newLst == null || oldLst.Count != newLst.Count)
            {
                return false;
            }

            for (int i = 0; i < newLst.Count; i++)
            {
                if (!oldLst[i].Equals(newLst[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public enum ActionType
    {
        Add,
        Update,
        Remove
    }
}
