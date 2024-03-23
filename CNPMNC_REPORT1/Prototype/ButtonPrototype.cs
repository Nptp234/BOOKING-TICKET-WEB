using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Prototype
{
    public abstract class ButtonPrototype
    {
        public string ID { get; set; }
        public string Class { get; set; }
        public string Text { get; set; }

        public abstract ButtonPrototype Clone();
    }
}