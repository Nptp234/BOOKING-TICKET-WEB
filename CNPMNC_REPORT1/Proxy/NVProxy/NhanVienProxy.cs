using CNPMNC_REPORT1.Composite.NVComposite;
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
            // Danh sách các trang quản lý được phân loại
            //List<string> managedPages = new List<string>();
            //List<string> dsLNV = sQLUser.LayDSLoaiNhanVienTuMaNV(nhanVien.MaNV);

            //if (dsLNV != null)
            //{
            //    foreach (string tenLNV in dsLNV)
            //    {
            //        phanTrangNV = PhanTrangNV.XacDinhNVQL(tenLNV);

            //        if (phanTrangNV != null)
            //        {
            //            foreach (string trang in phanTrangNV.LayDSTrangTheoNV())
            //            {
            //                if (!managedPages.Contains(trang) && trang != "IndexNull" && trang != "HDDetail") // Kiểm tra trang đã tồn tại trong danh sách chưa
            //                {
            //                    managedPages.Add(trang);
            //                }
            //            }
            //        }
            //    }
            //}
            //else return null;

            //return managedPages;

            return PhanTrang();
        }

        //----------------------------------------------------------------

        private FolderStorage folderStorage = FolderStorage.Instance;
        private Dictionary<FolderNV, List<AComponent>> Folder;
        private FolderNV folder;

        public List<string> PhanTrang()
        {
            // Danh sách các trang quản lý được phân loại
            List<string> managedPages = new List<string>();
            List<string> dsLNV = sQLUser.LayDSLoaiNhanVienTuMaNV(nhanVien.MaNV);

            folder = new FolderNV();

            if (dsLNV != null)
            {
                foreach (string lnv in dsLNV)
                {
                    phanTrangNV = PhanTrangNV.XacDinhNVQL(lnv);

                    folder = new FolderNV(lnv);
                    managedPages.AddRange(folderStorage.GetPageWithFolder(folder.GetName()));
                }
            }
            else return null;

            return managedPages;
        }
    }
}