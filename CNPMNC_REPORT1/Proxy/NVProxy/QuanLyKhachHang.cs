using CNPMNC_REPORT1.Composite.NVComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyKhachHang : PhanTrangNV
    {
        FolderNV managedPages = new FolderNV("QuanLyKhachHang");
        PageNV kh, khType, reportHD;
        List<AComponent> lstNV = new List<AComponent>();
        FolderStorage storage = FolderStorage.Instance;

        public QuanLyKhachHang()
        {
            SetupFolderNV();
            CapNhatTrangNV();

            SetupFolderStorage();
        }

        public override List<string> LayDSTrangTheoNV()
        {
            //List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            //// Thêm tên các trang quản lý đã phân loại vào danh sách
            //managedPages.Add("KhachHang");
            //managedPages.Add("KHType");
            //managedPages.Add("ReportHD");

            //return managedPages;


            return storage.GetPageWithFolder(managedPages.GetName());
        }

        public override void SetupFolderNV()
        {
            kh = new PageNV("KhachHang");
            khType = new PageNV("KHType");
            reportHD = new PageNV("ReportHD");
            lstNV = new List<AComponent> { kh, khType, reportHD };

            managedPages.AddListPage(lstNV);
        }

        public override void SetupFolderStorage()
        {
            storage.RemoveFolder(managedPages.GetName());
            storage.SaveFolder(managedPages.GetName(), managedPages.GetListComponentPage());
        }

        private void CapNhatTrangNV()
        {
            lstNV.Add(new PageNV("AgeLimit"));
            managedPages.AddListPage(lstNV);
        }
    }
}