using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Iterator.PhimIterator
{
    public class TimKiemPhimCollect : IterableCollection
    {
        private List<Phim> phimList;

        public TimKiemPhimCollect(List<Phim> phimList)
        {
            this.phimList = phimList;
        }

        public Iterator CreateIterator()
        {
            return new TimKiemPhimIterator(phimList);
        }

        public int Count
        {
            get { return phimList.Count; }
        }

        public Phim this[int index]
        {
            get { return phimList[index]; }
        }
    }
}