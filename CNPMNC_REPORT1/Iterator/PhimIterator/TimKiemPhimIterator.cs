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

namespace CNPMNC_REPORT1.Iterator.PhimIterator
{
    public class TimKiemPhimIterator : Iterator
    {
        private List<Phim> list;
        private int currentIndex;

        public TimKiemPhimIterator(List<Phim> newList)
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

        public Phim Current
        {
            get { return list[currentIndex]; }
        }

        public List<Phim> SearchPhimTheoTen(string searchValue)
        {
            return list
                .Where(p => p.TenPhim != null && p.TenPhim.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<Phim> SearchPhimTheoMaP(string searchValue)
        {
            return list
                .Where(p => p.MaPhim != null && p.MaPhim.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<Phim> SearchPhimTheoTL(string searchValue)
        {
            // Lấy danh sách loại phim giống search
            LoaiPhimFactory loaiPhimFactory = new CreateAllLP();
            List<LoaiPhim> loaiPhimList = loaiPhimFactory.CreateLoaiP().Where(p => p.TenTL != null && p.TenTL.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            // Kiểm tra xem có loại phim phù hợp hay không
            if (loaiPhimList.Count == 0)
            {
                return new List<Phim>();
            }

            // Lấy mã thể loại của loại phim tìm được
            string maTL = loaiPhimList[0].MaTL;

            // Lấy danh sách thể loại và phim
            TLVPFactory tlvpFactory = new CreateAllTLVP();
            List<TheLoaiVaPhim> tlvpList = tlvpFactory.CreateTLVP().Where(p => p.MaTL != null && p.MaTL.IndexOf(maTL, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            // Lấy danh sách phim theo thể loại
            PhimFactory phimFactory = new CreateAllPhim();
            List<Phim> phimList = new List<Phim>();
            foreach (var i in tlvpList)
            {
                // Tìm phim theo mã phim từ tlvp
                Phim phim = phimFactory.CreatePhim()
                    .FirstOrDefault(p => p.MaPhim != null && p.MaPhim.IndexOf(i.MaPhim, StringComparison.OrdinalIgnoreCase) >= 0);

                // Nếu tìm thấy phim, thêm vào danh sách phim
                if (phim != null)
                {
                    phimList.Add(phim);
                }
            }

            return phimList;
        }

    }
}