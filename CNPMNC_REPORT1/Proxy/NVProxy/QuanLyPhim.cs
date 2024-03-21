using CNPMNC_REPORT1.Composite.NVComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyPhim : PhanTrangNV
    {
        FolderNV managedPages = new FolderNV("QuanLyPhim");
        PageNV phim, lp, ght, tlvp;
        List<AComponent> lst = new List<AComponent>();
        FolderStorage storage = FolderStorage.Instance;

        public override List<string> LayDSTrangTheoNV()
        {
            //List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            //// Thêm tên các trang quản lý đã phân loại vào danh sách
            //managedPages.Add("Film");
            //managedPages.Add("AgeLimit");
            //managedPages.Add("FilmType");
            //managedPages.Add("TheLoaiVaPhim");

            //return managedPages;

            phim = new PageNV("Film");
            ght = new PageNV("AgeLimit");
            lp = new PageNV("FilmType");
            tlvp = new PageNV("TheLoaiVaPhim");
            lst = new List<AComponent> { phim, ght, lp, tlvp };

            managedPages.AddListPage(lst);

            storage.RemoveFolder(managedPages);
            storage.SaveFolder(managedPages, managedPages.GetListComponentPage());

            return storage.GetPageWithFolder(managedPages);
        }
    }
}