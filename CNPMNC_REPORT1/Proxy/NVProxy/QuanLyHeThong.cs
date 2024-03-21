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

            foreach(var children in storage.GetAllFolder())
            {
                lst.AddRange(children.Value);
            }
            managedPages.AddListPage(lst);

            storage.RemoveFolder(managedPages);
            storage.SaveFolder(managedPages, managedPages.GetListComponentPage());

            return storage.GetPageWithFolder(managedPages);
        }
    }
}