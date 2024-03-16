using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Memento.CaretakerFolder
{
    public class GheCaretaker
    {
        private IMemento _memento;

        public void SaveState(IMemento memento)
        {
            _memento = memento;
        }

        public IMemento GetSavedState()
        {
            return _memento;
        }
    }
}