using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Iterator.BLIterator
{
    public class BLCollect : IterableCollection
    {
        private List<BinhLuan> List;

        public BLCollect(List<BinhLuan> list)
        {
            this.List = list;
        }

        public Iterator CreateIterator()
        {
            return new BLIterator(this);
        }

        public int Count
        {
            get { return List.Count; }
        }

        public BinhLuan this[int index]
        {
            get { return List[index]; }
        }
    }
}