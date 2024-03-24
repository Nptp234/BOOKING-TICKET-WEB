using CNPMNC_REPORT1.Composite.NVComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyNhanVien : PhanTrangNV
    {
        FolderNV managedPages = new FolderNV("QuanLyNhanVien");
        PageNV nv, nvtype;
        List<AComponent> lst = new List<AComponent>();
        FolderStorage storage = FolderStorage.Instance;

        public QuanLyNhanVien()
        {
            SetupFolderNV();
            SetupFolderStorage();
        }

        public override List<string> LayDSTrangTheoNV()
        {
            //List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            //// Thêm tên các trang quản lý đã phân loại vào danh sách
            //managedPages.Add("NhanVien");
            //managedPages.Add("NVType");

            //return managedPages;

            return storage.GetPageWithFolder(managedPages.GetName());
        }

        public override void SetupFolderNV()
        {
            nv = new PageNV("NhanVien");
            nvtype = new PageNV("NVType");
            lst = new List<AComponent> { nv, nvtype };

            managedPages.AddListPage(lst);
        }

        public override void SetupFolderStorage()
        {
            storage.RemoveFolder(managedPages.GetName());
            storage.SaveFolder(managedPages.GetName(), managedPages.GetListComponentPage());
        }
    }
}