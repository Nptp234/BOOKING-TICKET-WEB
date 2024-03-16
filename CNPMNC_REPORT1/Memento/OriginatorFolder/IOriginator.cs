using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Memento.OriginatorFolder
{
    public interface IOriginator
    {
        IMemento Save();
    }
}