using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer
{
    public class SubjectObserver
    {
        public T Object { get; set; }
        private List<IObserver> _observers = new List<IObserver>();

        public SubjectObserver() { }
        public SubjectObserver(T obj)
        {
            this.Object = obj;
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(object obj, ActionType actionType)
        {
            foreach (var observer in _observers)
            {
                observer.PerformAction(obj, actionType);
            }
        }
    }
}