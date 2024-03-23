using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Observer.Subject
{
    public class XuatChieuSubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public XuatChieuSubject() { }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void DetachAll()
        {
            _observers.Clear();
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