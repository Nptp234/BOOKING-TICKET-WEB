using CNPMNC_REPORT1.Composite.NVComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyPhongChieu : PhanTrangNV
    {
        FolderNV managedPages = new FolderNV("QuanLyPhongChieu");
        PageNV pc, lpc;
        List<AComponent> lst = new List<AComponent>();
        FolderStorage storage = FolderStorage.Instance;

        public QuanLyPhongChieu()
        {
            SetupFolderNV();
            SetupFolderStorage();
        }

        public override List<string> LayDSTrangTheoNV()
        {
            //List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            //// Thêm tên các trang quản lý đã phân loại vào danh sách
            //managedPages.Add("Room");
            //managedPages.Add("RoomType");

            //return managedPages;


            return storage.GetPageWithFolder(managedPages.GetName());
        }

        public override void SetupFolderNV()
        {
            pc = new PageNV("Room");
            lpc = new PageNV("RoomType");
            lst = new List<AComponent> { pc, lpc };

            managedPages.AddListPage(lst);
        }

        public override void SetupFolderStorage()
        {
            storage.RemoveFolder(managedPages.GetName());
            storage.SaveFolder(managedPages.GetName(), managedPages.GetListComponentPage());
        }
    }
}