using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Composite
{
    public abstract class AbstractOne
    {
        public string pageName { get; set; }
        public abstract string ListPage();
    }
}