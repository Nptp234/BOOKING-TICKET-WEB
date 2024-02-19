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
        public override List<string> LayDSTrangTheoNV()
        {
            List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            string folderPath = HostingEnvironment.MapPath("~/Areas/AdminArea/Views/Admin");

            if (Directory.Exists(folderPath))
            {
                string[] cshtmlFiles = Directory.GetFiles(folderPath, "*.cshtml");

                managedPages.AddRange(cshtmlFiles.Select(Path.GetFileNameWithoutExtension));
            }

            return managedPages;
        }
    }
}