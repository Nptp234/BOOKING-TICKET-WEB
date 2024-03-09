using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Iterator
{
    public interface IterableCollection
    {
        Iterator CreateIterator();
    }
}