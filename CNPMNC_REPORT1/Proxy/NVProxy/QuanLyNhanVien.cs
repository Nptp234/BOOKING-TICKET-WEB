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

        public override List<string> LayDSTrangTheoNV()
        {
            //List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            //// Thêm tên các trang quản lý đã phân loại vào danh sách
            //managedPages.Add("NhanVien");
            //managedPages.Add("NVType");

            //return managedPages;

            nv = new PageNV("NhanVien");
            nvtype = new PageNV("NVType");
            lst = new List<AComponent> { nv, nvtype };

            managedPages.AddListPage(lst);

            storage.RemoveFolder(managedPages);
            storage.SaveFolder(managedPages, managedPages.GetListComponentPage());

            return storage.GetPageWithFolder(managedPages);
        }
    }
}