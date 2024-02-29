using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Composite
{
    public class FileCompo : AbstractOne
    {
        public FileCompo(string name)
        {
            this.pageName = name;
        }
        public override string ListPage()
        {
            return this.pageName;
        }
    }
}