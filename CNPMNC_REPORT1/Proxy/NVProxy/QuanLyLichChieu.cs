using CNPMNC_REPORT1.Composite.NVComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyLichChieu : PhanTrangNV
    {
        FolderNV managedPages = new FolderNV("QuanLyLichChieu");
        PageNV lc, xc, phim, pc;
        List<AComponent> lst = new List<AComponent>();
        FolderStorage storage = FolderStorage.Instance;

        public QuanLyLichChieu()
        {
            SetupFolderNV();
            SetupFolderStorage();
        }

        public override List<string> LayDSTrangTheoNV()
        {
            //List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            //// Thêm tên các trang quản lý đã phân loại vào danh sách
            //managedPages.Add("LichChieu");
            //managedPages.Add("XuatChieu");
            //managedPages.Add("Film");
            //managedPages.Add("Room");

            //return managedPages;

            return storage.GetPageWithFolder(managedPages.GetName());
        }

        public override void SetupFolderNV()
        {
            lc = new PageNV("LichChieu");
            xc = new PageNV("XuatChieu");
            phim = new PageNV("Film");
            pc = new PageNV("Room");
            lst = new List<AComponent> { lc, xc, phim, pc };

            managedPages.AddListPage(lst);
        }

        public override void SetupFolderStorage()
        {
            storage.RemoveFolder(managedPages.GetName());
            storage.SaveFolder(managedPages.GetName(), managedPages.GetListComponentPage());
        }
    }
}