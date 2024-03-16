using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Iterator.YTIterator
{
    public class YTIterator : Iterator
    {
        private YTCollect list;
        private int currentIndex;

        public YTIterator(YTCollect newList)
        {
            list = newList;
            currentIndex = 0;
        }

        public void GetNext()
        {
            currentIndex++;
        }

        public bool HasMore()
        {
            return currentIndex < list.Count;
        }

        public YeuThich Current
        {
            get { return list[currentIndex]; }
        }

        // Hàm sắp xếp theo thông tin lượt mua của Phim (tăng dần)
        public List<YeuThich> SortMostLiked()
        {
            List<YeuThich> sorted = new List<YeuThich>();
            while (HasMore())
            {
                sorted.Add(Current);
                GetNext();
            }

            // Bubble Sort
            for (int i = 1; i < sorted.Count; i++)
            {
                YeuThich key = sorted[i];
                int j = i - 1;

                while (j >= 0 && int.Parse(sorted[j].ThongTinPhim.LuotMua) < int.Parse(key.ThongTinPhim.LuotMua))
                {
                    sorted[j + 1] = sorted[j];
                    j = j - 1;
                }

                sorted[j + 1] = key;
            }

            return sorted;
        }

        // Hàm sắp xếp theo thông tin lượt mua của Phim (giảm dần)
        public List<YeuThich> SortLeastLiked()
        {
            List<YeuThich> sorted = new List<YeuThich>();
            while (HasMore())
            {
                sorted.Add(Current);
                GetNext();
            }

            // Bubble Sort
            for (int i = 1; i < sorted.Count; i++)
            {
                YeuThich key = sorted[i];
                int j = i - 1;

                while (j >= 0 && int.Parse(sorted[j].ThongTinPhim.LuotMua) > int.Parse(key.ThongTinPhim.LuotMua))
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