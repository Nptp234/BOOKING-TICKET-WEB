using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyPhim : PhanTrangNV
    {
        public QuanLyPhim() { }

        public override List<string> LayDSTrangTheoNV()
        {
            List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            // Thêm tên các trang quản lý đã phân loại vào danh sách
            managedPages.Add("Film");
            managedPages.Add("AgeLimit");
            managedPages.Add("FilmType");
            managedPages.Add("TheLoaiVaPhim");

            return managedPages;
        }
    }
}