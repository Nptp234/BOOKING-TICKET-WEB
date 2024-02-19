using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public abstract class PhanTrangNV
    {
        public abstract List<string> LayDSTrangTheoNV();

        public static PhanTrangNV XacDinhNVQL(string tenLNV)
        {
            // Lấy tên của lớp con dựa trên tên được cung cấp
            string tenLopCon = $"CNPMNC_REPORT1.Proxy.NVProxy.{tenLNV.Replace(" ", string.Empty)}";

            // Sử dụng Reflection để tạo đối tượng của lớp con
            Type type = Type.GetType(tenLopCon);
            if (type != null && typeof(PhanTrangNV).IsAssignableFrom(type) && !type.IsAbstract)
            {
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