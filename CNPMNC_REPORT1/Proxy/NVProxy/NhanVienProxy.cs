using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Proxy.NVProxy
{
    public class NhanVienProxy : INhanVienProxy
    {
        private NhanVien nhanVien;
        private PhanTrangNV phanTrangNV;
        private SQLUser sQLUser;

        public NhanVienProxy(NhanVien _nhanVien)
        {
            nhanVien = _nhanVien;
            sQLUser = SQLUser.Instance;
        }

        public List<string> PhanLoaiTrangTheoLNV()
        {
            List<string> managedPages = new List<string>(); // Danh sách các trang quản lý được phân loại
            List<string> dsLNV = sQLUser.LayDSLoaiNhanVienTuMaNV(nhanVien.MaNV);

            if (dsLNV != null)
            {
                //foreach (string tenLNV in dsLNV)
                //{
                //    phanTrangNV = PhanTrangNV.XacDinhNVQL(tenLNV);

                //    if (phanTrangNV != null)
                //    {
                //        //managedPages = phanTrangNV.LayDSTrangTheoNV();
                //        managedPages.AddRange(phanTrangNV.LayDSTrangTheoNV());
                //    }
                //}

                foreach (string tenLNV in dsLNV)
                {
                    phanTrangNV = PhanTrangNV.XacDinhNVQL(tenLNV);

                    if (phanTrangNV != null)
                    {
                        foreach (string trang in phanTrangNV.LayDSTrangTheoNV())
                        {
                            if (!managedPages.Contains(trang)) // Kiểm tra trang đã tồn tại trong danh sách chưa
                            {
                                managedPages.Add(trang);
                            }
                        }
                    }
                }
            }
            else return null;

            return managedPages;
        }
    }
}