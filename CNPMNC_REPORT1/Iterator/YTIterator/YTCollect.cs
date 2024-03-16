using CNPMNC_REPORT1.Iterator.PhimIterator;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Iterator.YTIterator
{
    public class YTCollect : IterableCollection
    {
        private List<YeuThich> YTList;

        public YTCollect(List<YeuThich> YTList)
        {
            this.YTList = YTList;
        }

        public Iterator CreateIterator()
        {
            return new YTIterator(this);
        }

        public int Count
        {
            get { return YTList.Count; }
        }

        public YeuThich this[int index]
        {
            get { return YTList[index]; }
        }
    }
}