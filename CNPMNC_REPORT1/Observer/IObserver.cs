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
    }

    public enum ActionType
    {
        Add,
        Update,
        Remove
    }
}
