using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class QuanLyHeThong : PhanTrangNV
    {
        public override List<string> LayDSTrangTheoNV()
        {
            List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại

            // Lấy tên của tất cả các lớp con của PhanTrangNV
            var subclasses = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(PhanTrangNV)) && !t.IsAbstract);

            // Lặp qua từng lớp con và lấy tên của chúng
            foreach (var subclass in subclasses)
            {
                // Tạo một thực thể của lớp con hiện tại
                var instance = Activator.CreateInstance(subclass) as PhanTrangNV;
                // Lấy danh sách các trang từ lớp con và thêm vào danh sách managedPages
                managedPages.AddRange(instance.LayDSTrangTheoNV());
            }

            return managedPages;
        }
    }
}