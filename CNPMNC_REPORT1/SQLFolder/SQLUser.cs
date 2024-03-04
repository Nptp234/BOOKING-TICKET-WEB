using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLUser : SQLObject
    {
        public UserAccount User { get; private set; }
        public KhachHang KH { get; private set; }
        public NhanVien NV { get; private set; }

        private static SQLUser instance;
        private static readonly object lockObject = new object();

        private SQLUser()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public static SQLUser Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SQLUser();
                    }
                    return instance;
                }
            }
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public bool KiemTraThongTinDangNhap(string name, string pass, string type)
        {
            switch (type)
            {
                case "KhachHang":
                    return KiemTraDNKhachHang(name, pass);

                case "NhanVien":
                    return KiemTraDNNhanVien(name, pass);

                default: return false;
            }
        }

        public bool KiemTraDNKhachHang(string name, string pass)
        {
            string query = $"SELECT * FROM KHACHHANG WHERE TenTKKH = '{name}' AND MatKhauKH = '{pass}'";

            List<KhachHang> lsKH = new List<KhachHang>();

            lsKH = LayDS<KhachHang>(query);

            if (lsKH.Count > 0)
            {
                KH = lsKH[0];
                return true;
            }
            else return false;
        }

        public bool KiemTraDNNhanVien(string name, string pass)
        {
            string query = $"SELECT * FROM NHANVIEN WHERE Email = '{name}' AND MatKhauNV = '{pass}'";

            List<NhanVien> lsNV = new List<NhanVien>();

            lsNV = LayDS<NhanVien>(query);

            if (lsNV.Count > 0)
            {
                NV = lsNV[0];
                return true;
            }
            else return false;
        }

        private bool ThucHienTruyVanThemKH(KhachHang kh)
        {
            string query = $"INSERT INTO KHACHHANG VALUES('{kh.TenTKKH}', '{kh.MatKhauKH}', '{kh.EmailKH}', '{kh.DiemThuongKH}', '{kh.TrangThaiTKKH}', '{kh.MaLoaiKH}')";
            
            return ThucHienTruyVan(query);
        }

        public bool ThemKH(KhachHang kh)
        {
            if (ThucHienTruyVanThemKH(kh))
            {
                string query = $"SELECT * FROM KHACHHANG WHERE TenTKKh = '{kh.TenTKKH}'";
                List<KhachHang> ls = LayDS<KhachHang>(query);
                KH = ls[0];

                return true;
            }
            else return false;
        }

        public bool CapNhatKH(KhachHang kh)
        {
            KH = kh;

            bool updateEmail = CapNhatEmailKH(kh);
            bool updatePass = CapNhatMatKhauKH(kh);
            bool updateName = CapNhatTenNDKH(kh);

            if (updateEmail && updatePass && updateName)
            {
                return true;
            }
            else return false;
        }

        public bool CapNhatEmailKH(KhachHang kh)
        {
            string query = $"UPDATE KHACHHANG SET EmailKH='{kh.EmailKH}' WHERE MaKH = '{kh.MaKH}'";
            
            if (KiemTraEmailNDKH(kh.EmailKH))
            {
                KH = kh;
                return ThucHienTruyVan(query);
            }
            else return false;
        }

        public bool CapNhatMatKhauKH(KhachHang kh)
        {
            string query = $"UPDATE KHACHHANG SET MatKhauKH='{kh.MatKhauKH}' WHERE MaKH = '{kh.MaKH}'";
            KH = kh;
            return ThucHienTruyVan(query);
        }

        public bool CapNhatTenNDKH(KhachHang kh)
        {
            string query = $"UPDATE KHACHHANG SET TenTKKH='{kh.TenTKKH}' WHERE MaKH = '{kh.MaKH}'";
            
            if (KiemTraTenNDKH(kh.TenTKKH))
            {
                KH = kh;
                return ThucHienTruyVan(query);
            }
            else return false;
        }

        public bool KiemTraTenNDKH(string name)
        {
            string query = $"SELECT * FROM KHACHHANG WHERE TenTKKH = '{name}'";

            List<KhachHang> lsKH = new List<KhachHang>();

            lsKH = LayDS<KhachHang>(query);

            if (lsKH.Count > 0)
            {
                return false;
            }
            else return true;
        }

        public bool KiemTraEmailNDKH(string email)
        {
            string query = $"SELECT * FROM KHACHHANG WHERE EmailKH = '{email}'";

            List<KhachHang> lsKH = new List<KhachHang>();

            lsKH = LayDS<KhachHang>(query);

            if (lsKH.Count > 0)
            {
                return false;
            }
            else return true;
        }

        public List<PhanQuyenNV> LayDanhSachPhanQuyen(string maNV)
        {
            string query = $"SELECT * FROM PHANQUYENNV WHERE MaNV = '{maNV}'";

            List<PhanQuyenNV> lsPQ = new List<PhanQuyenNV>();

            lsPQ = LayDS<PhanQuyenNV>(query);

            if (lsPQ.Count > 0)
            {
                return lsPQ;
            }
            else return null;
        }

        public string LayTenLoaiNhanVienTuMaLNV(string maLoaiNV)
        {
            string query = $"SELECT TenLNV FROM LOAITKNV WHERE MaLNV = '{maLoaiNV}'";

            List<LoaiNV> lsLNV = new List<LoaiNV>();

            lsLNV = LayDS<LoaiNV>(query);

            if (lsLNV.Count > 0)
            {
                return lsLNV[0].TenLNV;
            }
            else return null;
        }

        public string LayTenLoaiKhachHangTuMaLoaiKH(string maLoaiKH)
        {
            string query = $"SELECT TenLKH FROM LOAIKH WHERE MaLoaiKH = '{maLoaiKH}'";

            List<LoaiKH> lsLKH = new List<LoaiKH>();

            lsLKH = LayDS<LoaiKH>(query);

            if (lsLKH.Count > 0)
            {
                return lsLKH[0].TenLKH;
            }
            else return null;
        }

        public List<string> LayDSLoaiNhanVienTuMaNV(string maNV)
        {
            // Lấy danh sách phân quyền của nhân viên từ cơ sở dữ liệu
            List<PhanQuyenNV> danhSachPhanQuyen = LayDanhSachPhanQuyen(maNV);

            List<string> dsLNV = new List<string>();

            if (danhSachPhanQuyen == null)
            {
                return null;
            }

            // Tìm loại nhân viên trong danh sách phân quyền
            foreach (PhanQuyenNV phanQuyen in danhSachPhanQuyen)
            {
                if (phanQuyen.MaNV == maNV)
                {
                    string maLoaiNV = phanQuyen.MaLNV;

                    // Tìm tên loại nhân viên tương ứng với mã loại nhân viên
                    string tenLoaiNV = LayTenLoaiNhanVienTuMaLNV(maLoaiNV);

                    // Thêm tên vào danh sách
                    dsLNV.Add(tenLoaiNV);
                }
            }

            // Trả về ds loại nhân viên
            return dsLNV;
        }

        public string LayChietKhauTuMaLoaiKH(string maLoaiKH)
        {
            string query = $"SELECT ChietKhau FROM LOAIKH WHERE MaLoaiKH = '{maLoaiKH}'";

            List<LoaiKH> lsLKH = new List<LoaiKH>();

            lsLKH = LayDS<LoaiKH>(query);

            if (lsLKH.Count > 0)
            {
                return lsLKH[0].ChietKhau;
            }
            else return null;
        }
    }
}