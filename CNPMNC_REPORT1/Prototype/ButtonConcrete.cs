using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Prototype
{
    public class ButtonConcrete:ButtonPrototype
    {
        public override ButtonPrototype Clone()
        {
            return (ButtonPrototype)this.MemberwiseClone();
        }
    }
}