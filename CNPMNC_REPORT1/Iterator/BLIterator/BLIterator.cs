using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Iterator.BLIterator
{
    public class BLIterator : Iterator
    {
        private BLCollect Collect;
        private int currentIndex;

        public BLIterator(BLCollect collect)
        {
            Collect = collect;
            currentIndex = 0;
        }

        public void GetNext()
        {
            currentIndex++;
        }

        public bool HasMore()
        {
            return currentIndex < Collect.Count - 1;
        }

        public void ResetIterator()
        {
            currentIndex = 0;
        }

        public BinhLuan Current
        {
            get { return Collect[currentIndex]; }
        }

        public List<BinhLuan> SortNewest()
        {
            List<BinhLuan> sorted = new List<BinhLuan>();
            while (HasMore())
            {
                sorted.Add(Current);
                GetNext();
            }

            // Bubble Sort
            for (int i = 1; i < sorted.Count; i++)
            {
                BinhLuan key = sorted[i];
                int j = i - 1;

                while (j >= 0 && DateTime.Parse(sorted[j].NgayTao) < DateTime.Parse(key.NgayTao))
                {
                    sorted[j + 1] = sorted[j];
                    j = j - 1;
                }

                sorted[j + 1] = key;
            }

            return sorted;
        }

        public List<BinhLuan> SortOldest()
        {
            List<BinhLuan> sorted = new List<BinhLuan>();
            while (HasMore())
            {
                sorted.Add(Current);
                GetNext();
            }

            // Bubble Sort
            for (int i = 1; i < sorted.Count; i++)
            {
                BinhLuan key = sorted[i];
                int j = i - 1;

                while (j >= 0 && DateTime.Parse(sorted[j].NgayTao) > DateTime.Parse(key.NgayTao))
                {
                    sorted[j + 1] = sorted[j];
                    j = j - 1;
                }

                sorted[j + 1] = key;
            }

            return sorted;
        }
    }
}