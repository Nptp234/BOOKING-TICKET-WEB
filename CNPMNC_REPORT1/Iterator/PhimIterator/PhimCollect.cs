using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Iterator.PhimIterator
{
    public class PhimCollect : IterableCollection
    {
        private List<Phim> PhimList;

        public PhimCollect(List<Phim> phimList)
        {
            this.PhimList = phimList;
        }

        public Iterator CreateIterator()
        {
            return new TimKiemPhimIterator(this);
        }

        public int Count
        {
            get { return PhimList.Count; }
        }

        public Phim this[int index]
        {
            get { return PhimList[index]; }
        }
    }
}