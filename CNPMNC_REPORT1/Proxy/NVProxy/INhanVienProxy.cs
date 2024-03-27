using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public interface INhanVienProxy
    {
        List<string> PhanLoaiTrangTheoLNV();
        bool PhanQuyenQL();
    }
}