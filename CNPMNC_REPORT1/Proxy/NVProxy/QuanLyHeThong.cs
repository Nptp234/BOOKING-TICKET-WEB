using CNPMNC_REPORT1.Composite.NVComposite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyHeThong : PhanTrangNV
    {
        FolderNV managedPages = new FolderNV("QuanLyHeThong");
        PageNV kh, khType, reportHD;
        List<AComponent> lst = new List<AComponent>();
        FolderStorage storage = FolderStorage.Instance;

        public QuanLyHeThong()
        {
            SetupFolderNV();
            SetupFolderStorage();
        }

        public override List<string> LayDSTrangTheoNV()
        {
            //List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            //string folderPath = HostingEnvironment.MapPath("~/Areas/AdminArea/Views/Admin");

            //if (Directory.Exists(folderPath))
            //{
            //    string[] cshtmlFiles = Directory.GetFiles(folderPath, "*.cshtml");

            //    managedPages.AddRange(cshtmlFiles.Select(Path.GetFileNameWithoutExtension));
            //}

            //return managedPages;

            return storage.GetPageWithFolder(managedPages.GetName());
        }

        public override void SetupFolderNV()
        {
            foreach (var children in storage.GetAllFolder())
            {
                lst.AddRange(children.Value);
            }
            managedPages.AddListPage(lst);
        }

        public override void SetupFolderStorage()
        {
            storage.RemoveFolder(managedPages.GetName());
            storage.SaveFolder(managedPages.GetName(), managedPages.GetListComponentPage());
        }
    }
}