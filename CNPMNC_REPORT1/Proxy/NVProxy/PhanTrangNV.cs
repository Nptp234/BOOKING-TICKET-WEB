using CNPMNC_REPORT1.Composite;
using CNPMNC_REPORT1.Composite.NVComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    // Nhiệm vụ của class là tự động tạo truy cập tới class con
    public abstract class PhanTrangNV
    {
        public abstract List<string> LayDSTrangTheoNV();
        public abstract void SetupFolderNV();
        public abstract void SetupFolderStorage();

        // Người viết Phuoc.
        // Lý do tạo hàm này:
        //  + Không cần tạo nhiều hàm khác nhau phía dưới
        //  + Hàm với mục đích tạo truy cập tới class có tên giống tên được truyền vào
        //  + Hàm có khả năng truy cập vào và thực hiện logic của lớp con

        // Lưu ý: Nếu có thay đổi => viết hàm mới
        public static PhanTrangNV XacDinhNVQL(string tenLNV)
        {
            // Lấy tên của lớp con dựa trên tên trong biến tenLNV, có chuẩn hóa tenLNV
            string tenLopCon = $"CNPMNC_REPORT1.Proxy.NVProxy.{tenLNV.Replace(" ", string.Empty)}";
            
            // Biến type sẽ lấy kiểu của lớp con
            Type type = Type.GetType(tenLopCon);

            // Kiểm tra xem lớp con !=null tức có tồn tại
            if (type != null)
            {
                // Tạo đường dẫn đến lớp con
                return (PhanTrangNV)Activator.CreateInstance(type);
            }
            else
            {
                //throw new ArgumentException("Tên loại nhân viên không hợp lệ hoặc không tìm thấy lớp con tương ứng.");
                return null;
            }
        }
    }
}