using CNPMNC_REPORT1.Memento.OriginatorFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Memento
{
    public class GheMemento : IMemento
    {
        private List<string> DsGhe { get; set; }
        private GheOriginator gheOriginator;

        public GheMemento(List<string> dsGhe)
        {
            DsGhe = dsGhe;
        }

        public List<string> GetDS()
        {
            return DsGhe;
        }

        public void Restore()
        {
            gheOriginator.SetListChair(DsGhe);
        }
    }
}