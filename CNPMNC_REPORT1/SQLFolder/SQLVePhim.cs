using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.VePhim;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CNPMNC_REPORT1.SQLFolder
{
    public class SQLVePhim : SQLObject
    {
        public SQLVePhim()
        {
            sQLConnection = SQLConnection.Instance;
        }



        public bool ThanhToanVP(string tonggia, string tentk)
        {
            string query = $"DECLARE @chietkhau FLOAT, @makh INT; " +
                           $"SELECT @chietkhau = lkh.ChietKhau " +
                           $"FROM KHACHHANG kh JOIN LOAIKH lkh ON kh.MaLoaiKH = lkh.MaLoaiKH " +
                           $"WHERE kh.TenTKKH = '{tentk}'; " +
                           $"SELECT @makh = MaKH FROM KHACHHANG WHERE TenTKKH = '{tentk}'; " +
                           $"INSERT INTO HOADON VALUES (GETDATE(), {tonggia}, {tonggia} - ({tonggia} * @chietkhau), @chietkhau, @makh, 1);";

            return ThucHienTruyVan(query);
        }

        public bool ThanhToanVG(string mave, string slve, string thanhtien)
        {
            string query = "DECLARE @mahd INT SELECT @mahd=MAX(MaHD) FROM HOADON; " +
                            $"INSERT INTO CHITIETHD VALUES ('{mave}', @mahd, '{slve}', '{thanhtien}')";

            return ThucHienTruyVan(query);
        }

        public bool CapNhatThanhToanVeP(string mave)
        {
            string query = $"UPDATE VEPHIM SET TRANGTHAITHANHTOAN = N'ĐÃ THANH TOÁN' WHERE MaVe = '{mave}';";

            return ThucHienTruyVan(query);
        }

        public bool XoaVePhim(string maVe)
        {
            string query = $"DELETE FROM VEPHIM WHERE MaVe = '{maVe}';";
            return ThucHienTruyVan(query);
        }

        public bool XoaVeGhe(string maVe)
        {
            string query = $"DELETE FROM VE_GHE WHERE MaVe = '{maVe}';";
            return ThucHienTruyVan(query);
        }

        public bool KiemTraXoaVe(string maVe)
        {
            string query = $"SELECT vp.* FROM VEPHIM vp JOIN CHITIETHD cthd ON cthd.MaVe = vp.MaVe WHERE vp.MaVe = '{maVe}';";
            List<VeP> lsVe = LayDS<VeP>(query);

            if (lsVe.Count==0)
            {
                bool isDel1 = XoaVeGhe(maVe);
                bool isDel2 = XoaVePhim(maVe);
                if (isDel1)
                {
                    return isDel2;
                }
                else return false;
            }
            else return false;
        }

        public List<VeP> LayDanhSachVePhimTuMaKH(string maKH)
        {
            string query = $"SELECT * FROM VEPHIM WHERE MaKH = '{maKH}'";

            List<VeP> lsVP = new List<VeP>();

            lsVP = LayDS<VeP>(query);

            if (lsVP.Count > 0)
            {
                return lsVP;
            }
            else return null;
        }

        public List<VeP> LayDanhSachVePChuaTTTuMaKH(string maKH)
        {
            string query = $"SELECT * FROM VEPHIM WHERE MaKH = '{maKH}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'";

            List<VeP> lsVP = new List<VeP>();

            lsVP = LayDS<VeP>(query);

            if (lsVP.Count > 0)
            {
                return lsVP;
            }
            else return null;
        }

        public List<VeP> LayVePhimLonNhat(string maKH)
        {
            string query = $"SELECT * FROM VEPHIM WHERE MaKH = '{maKH}' ORDER BY MaVe DESC";

            List<VeP> lsVP = new List<VeP>();

            lsVP = LayDS<VeP>(query);

            if (lsVP.Count > 0)
            {
                return lsVP;
            }
            else return null;
        }

        public List<VePhimChiTietKH> LayVePhimChoTTKH(string tenTKKH)
        {
            string query = QueryForLayVePhimChoTTKH(tenTKKH);

            List<VePhimChiTietKH> lsVP = new List<VePhimChiTietKH>();

            lsVP = LayDS<VePhimChiTietKH>(query);

            if (lsVP.Count > 0)
            {
                // vì tên ghế vg trả về chuỗi không đẹp nên thay bằng dấu ,
                foreach (var vePhim in lsVP)
                {
                    vePhim.TenGheVG = Regex.Replace(vePhim.TenGheVG, @"\s+", ", ");
                }

                return lsVP;
            }
            else return null;
        }

        private string QueryForLayVePhimChoTTKH(string tenTKKH)
        {
            return $"SELECT vp.MaVe, p.TenPhim, pc.TenPC, STRING_AGG(vg.TenGheVG, '') as TenGheVG, vp.NgayDat, lc.NgayLC, xc.GioXC " +
                   $"FROM VEPHIM vp, KHACHHANG kh, LICHCHIEU lc, PHIM p, PHONGCHIEU pc, VE_GHE vg, XUATCHIEU xc " +
                   $"WHERE kh.TenTKKH='{tenTKKH}' " +
                   $"AND vp.MaKH=kh.MaKH " +
                   $"AND vp.MaLC=lc.MaLC " +
                   $"AND lc.MaPhim=p.MaPhim " +
                   $"AND pc.MaPC=lc.MaPC " +
                   $"AND vp.MaVe=vg.MaVe " +
                   $"AND lc.MaXC=xc.MaXC " +
                   $"GROUP BY vp.MaVe, p.TenPhim, pc.TenPC, vp.NgayDat, lc.NgayLC, xc.GioXC " +
                   $"ORDER BY vp.NgayDat DESC";
        }

        public List<VeGhe> LayDanhSachGheVePhimTuMaVe(string maVe)
        {
            string query = $"SELECT * FROM VE_GHE WHERE MaVe = '{maVe}'";

            List<VeGhe> lsVG = new List<VeGhe>();

            lsVG = LayDS<VeGhe>(query);

            if (lsVG.Count > 0)
            {
                return lsVG;
            }
            else return null;
        }
    }
}