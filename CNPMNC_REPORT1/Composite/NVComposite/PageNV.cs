using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Composite.NVComposite
{
    public class PageNV : AComponent
    {
        private string PageName { get; set; }

        public PageNV(string name)
        {
            PageName = name;
        }

        public override string GetPage()
        {
            return PageName;
        }
    }
}