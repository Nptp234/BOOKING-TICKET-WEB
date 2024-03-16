using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNPMNC_REPORT1.Factory;
using CNPMNC_REPORT1.Factory.FactoryLoaiPhim;
using CNPMNC_REPORT1.Factory.FactoryPhim;
using CNPMNC_REPORT1.Factory.FactoryTLVP;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.Film;
using CNPMNC_REPORT1.SQLFolder;

namespace CNPMNC_REPORT1.Iterator.PhimIterator
{
    public class TimKiemPhimIterator : Iterator
    {
        private PhimCollect phimCollect;
        private int currentIndex;

        public TimKiemPhimIterator(PhimCollect collect)
        {
            phimCollect = collect;
            currentIndex = -1;
        }

        public void GetNext()
        {
            currentIndex++;
        }

        public bool HasMore()
        {
            return currentIndex < phimCollect.Count - 1;
        }

        public void ResetIterator()
        {
            currentIndex = -1;
        }

        public Phim Current
        {
            get { return phimCollect[currentIndex]; }
        }


        public List<Phim> TimPhimTheoTen(string searchValue)
        {
            List<Phim> searchResults = new List<Phim>();

            while (HasMore())
            {
                GetNext();
                if (Current.TenPhim != null && Current.TenPhim.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    searchResults.Add(Current);
                }
            }

            return searchResults;
        }

        public List<Phim> TimPhimTheoMaP(string searchValue)
        {
            List<Phim> searchResults = new List<Phim>();

            while (HasMore())
            {
                GetNext();
                if (Current.MaPhim != null && Current.MaPhim.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    searchResults.Add(Current);
                }
            }

            return searchResults;
        }

        public List<Phim> TimPhimTheoTL(string searchValue)
        {
            List<Phim> searchResults = new List<Phim>();

            // Lấy danh sách loại phim được tìm
            List<LoaiPhim> lpLst = LayDSLoaiPhimTheoSearch(searchValue);

            // Lấy danh sách mã phim có mã thể loại trùng danh sách trên
            List<TheLoaiVaPhim> lsTLVP = LayDSMaPhimTheoMaTL(lpLst);

            // Lấy danh sách phim và trả về
            foreach (var TLVP in lsTLVP)
            {
                while (HasMore())
                {
                    GetNext();
                    if (Current.MaPhim != null && Current.MaPhim.IndexOf(TLVP.MaPhim, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (!searchResults.Contains(Current))
                        {
                            searchResults.Add(Current);
                        }
                    }
                }
                ResetIterator();
            }

            return searchResults;
        }

        public List<TheLoaiVaPhim> LayDSMaPhimTheoMaTL(List<LoaiPhim> lst)
        {
            SQLTheLVP tlvp = new SQLTheLVP();
            List<TheLoaiVaPhim> lsTLVP = new List<TheLoaiVaPhim>();

            foreach(var tl in lst)
            {
                lsTLVP.AddRange(tlvp.LayDSMaPhimTheoMaTL(tl.MaTL));
            }

            return lsTLVP;
        }

        public List<LoaiPhim> LayDSLoaiPhimTheoSearch(string searchValue)
        {
            List<LoaiPhim> searchResults = new List<LoaiPhim>();

            LoaiPhimFactory lp = new CreateAllLP();
            foreach (var loai in lp.CreateLoaiP())
            {
                if (loai.TenTL.Contains(searchValue))
                {
                    searchResults.Add(loai);
                }
            }

            return searchResults;
        }

    }
}