using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections; // Sử dụng Lớp ArrayList để lưu kết quả
using System.Data.SqlClient;// Sử dụng các lớp tương tác CSDL
using CNPMNC_REPORT1.Models;
using System.IO;

namespace CNPMNC_REPORT1.Areas.AdminArea.Controllers
{
    public class AdminController : Controller
    {

        // GET: AdminArea/Admin
        

        public ActionResult Film()
        {
            SQLData data = new SQLData();
            ViewBag.DSGHTP = data.getData("SELECT * FROM GIOIHANTUOI");
            ViewBag.DSLPP = data.getData("SELECT * FROM THELOAIP");

            if (data.getData("SELECT * FROM PHIM") != null)
            {
                ViewBag.DSF = data.getData("SELECT * FROM PHIM");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Film(int? MaP, string TenF, string MoTaF, string NgayCC, int? ThoiLuongP, string HinhAnhP, string TrailerP, string GHTP, int? GiaP, int? MaGHT, HttpPostedFileBase HinhAnhFile, HttpPostedFileBase HinhAnhFiledetail1, string HinhAnhFiledetail2, string status)
        {
            SQLData data = new SQLData();
            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenF != null && MoTaF != null && NgayCC != null && ThoiLuongP != null && TrailerP != null && GHTP != null && GiaP != null)
                    {
                        if (HinhAnhFile != null)
                        {
                            var fileName = Path.GetFileName(HinhAnhFile.FileName);
                            var path = Path.Combine(Server.MapPath("~/img_phim"), fileName);
                            HinhAnhFile.SaveAs(path);

                            int getMaGHT = data.getMaGHT(GHTP);
                            if (getMaGHT != 0)
                            {
                                bool isSaved = data.saveFilm(TenF, MoTaF, NgayCC, ThoiLuongP, fileName, TrailerP, GiaP, getMaGHT);
                                if (!isSaved)
                                {
                                    ViewBag.ThongBaoLuu = "Lỗi lưu không thành công hoặc phim đã tồn tại!";
                                    ViewBag.DSF = data.getData("SELECT * FROM PHIM");
                                    ViewBag.DSGHTP = data.getData("SELECT * FROM GIOIHANTUOI");
                                }
                                else
                                {
                                    ViewBag.DSF = data.getData("SELECT * FROM PHIM");
                                    ViewBag.DSGHTP = data.getData("SELECT * FROM GIOIHANTUOI");
                                }
                            }
                            else
                            {
                                ViewBag.ThongBaoLuu = "Lỗi không tồn tại giới hạn tuổi!";
                                ViewBag.DSF = data.getData("SELECT * FROM PHIM");
                                ViewBag.DSGHTP = data.getData("SELECT * FROM GIOIHANTUOI");
                            }
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                            ViewBag.DSF = data.getData("SELECT * FROM PHIM");
                            ViewBag.DSGHTP = data.getData("SELECT * FROM GIOIHANTUOI");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi hinhanh ko tồn tại!";
                        ViewBag.DSF = data.getData("SELECT * FROM PHIM");
                        ViewBag.DSGHTP = data.getData("SELECT * FROM GIOIHANTUOI");
                    }
                }
                else if (status == "Update")
                {
                    if (TenF != null && MoTaF != null && NgayCC != null && TrailerP != null && ThoiLuongP != null && MaGHT != null && GiaP != null)
                    {
                        if (HinhAnhFiledetail1 == null)
                        {
                            
                            bool isUpdate = data.updateFilm(MaP, TenF, MoTaF, NgayCC, ThoiLuongP, HinhAnhFiledetail2, TrailerP, GiaP, MaGHT);
                            if (!isUpdate)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                                ViewBag.DSTLF = data.getData("SELECT * FROM PHIM");
                            }
                            else
                            {
                                ViewBag.DSTLF = data.getData("SELECT * FROM PHIM");
                            }
                        }
                        else
                        {
                            var fileName1 = Path.GetFileName(HinhAnhFiledetail1.FileName);
                            var path1 = Path.Combine(Server.MapPath("~/img"), fileName1);
                            HinhAnhFiledetail1.SaveAs(path1);

                            
                            bool isUpdate = data.updateFilm(MaP, TenF, MoTaF, NgayCC, ThoiLuongP, fileName1, TrailerP, GiaP, MaGHT);
                            if (!isUpdate)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                                ViewBag.DSTLF = data.getData("SELECT * FROM PHIM");
                            }
                            else
                            {
                                ViewBag.DSTLF = data.getData("SELECT * FROM PHIM");
                            }
                        }
                        
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = $"Lỗi null {MaP},{TenF},{MoTaF},{NgayCC},{ThoiLuongP},{TrailerP},{GiaP},{MaGHT}!";
                        ViewBag.DSTLF = data.getData("SELECT * FROM PHIM");
                    }
                }
                else ViewBag.ThongBaoLuu = "Lỗi model!";
            }
            return View();
        }

        public ActionResult FilmType()
        {
            SQLData data = new SQLData();
            ViewBag.DSTLF = data.getData("SELECT * FROM THELOAIP");
            return View();
        }
        [HttpPost]
        public ActionResult FilmType(int? MaTL,string TenLF, string MoTaLF, string status)
        {
            SQLData data = new SQLData();
            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenLF != null && MoTaLF != null)
                    {
                        ArrayList listfilmtype = data.getData($"SELECT * FROM THELOAIP WHERE TenTL=N'{TenLF}'");
                        if (listfilmtype.Count == 0)
                        {
                            bool isSaved = data.saveFilmType(TenLF, MoTaLF);
                            if (!isSaved)
                            {
                                @ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                            }
                            else ViewBag.DSTLF = data.getData("SELECT * FROM THELOAIP");
                        }
                        else
                        { 
                            ViewBag.ThongBaoLuu = "Đã tồn tại tên loại!";
                            ViewBag.DSTLF = data.getData("SELECT * FROM THELOAIP");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                        ViewBag.DSTLF = data.getData("SELECT * FROM THELOAIP");
                    }


                }
                else if (status == "Update")
                {
                    if (TenLF != null && MoTaLF != null)
                    {
                        bool isUpdate = data.updateFilmTypeDetail(MaTL, TenLF, MoTaLF);
                        if (!isUpdate)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                            ViewBag.DSTLF = data.getData("SELECT * FROM THELOAIP");
                        }
                        else
                        {
                            ViewBag.DSTLF = data.getData("SELECT * FROM THELOAIP");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                        ViewBag.DSTLF = data.getData("SELECT * FROM THELOAIP");
                    }
                }
                else ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
            }
            return View();
        }

        public ActionResult AgeLimit()
        {
            SQLData data = new SQLData();
            if (data.getData("SELECT * FROM GIOIHANTUOI")!=null)
            {
                ViewBag.DSGHT = data.getData("SELECT * FROM GIOIHANTUOI");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AgeLimit(int? MaGHT,string TenGHT,string MoTaGHT, string status)
        {
            SQLData data = new SQLData();
            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenGHT != null && MoTaGHT != null)
                    {
                        ArrayList listfilmtype = data.getData($"SELECT * FROM GIOIHANTUOI WHERE TenGHT=N'{TenGHT}'");
                        if (listfilmtype.Count == 0)
                        {
                            bool isSaved = data.saveAgeLimit(TenGHT, MoTaGHT);
                            if (!isSaved)
                            {
                                @ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                            }
                            else ViewBag.DSGHT = data.getData("SELECT * FROM GIOIHANTUOI");
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Đã tồn tại tên loại!";
                            ViewBag.DSGHT = data.getData("SELECT * FROM GIOIHANTUOI");
                        }
                        
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                        ViewBag.DSGHT = data.getData("SELECT * FROM GIOIHANTUOI");
                    }

                }
                else if (status == "Update")
                {
                    if (TenGHT != null && MoTaGHT != null)
                    {
                        bool isUpdate = data.updateAgeLimit(MaGHT, TenGHT, MoTaGHT);
                        if (!isUpdate)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                            ViewBag.DSGHT = data.getData("SELECT * FROM GIOIHANTUOI");
                        }
                        else
                        {
                            ViewBag.DSGHT = data.getData("SELECT * FROM GIOIHANTUOI");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                        ViewBag.DSGHT = data.getData("SELECT * FROM GIOIHANTUOI");
                    }
                }
                else ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
            }
            return View();
        }

        public ActionResult Room()
        {
            SQLData data = new SQLData();
            ViewBag.ListLcuaPC = data.getData("SELECT * FROM LOAIPC");

            if (data.getData("SELECT * FROM PHONGCHIEU") != null)
            {
                ViewBag.DSPC = data.getData("SELECT * FROM PHONGCHIEU");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Room(int? MaPC, string TenPC, int? SoLuongGT, int? SoLuongGV, string LoaiPC,int? MaLPC, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenPC != null && SoLuongGT > 0 && SoLuongGV > 0 && LoaiPC != null)
                    {
                        int getMaLPC = data.getMaLoaiPC(LoaiPC);
                        if (getMaLPC != 0)
                        {
                            bool isSaved = data.saveRoom(TenPC, SoLuongGT, SoLuongGV, getMaLPC);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công hoặc phòng chiếu đã tồn tại!";
                                ViewBag.DSPC = data.getData("SELECT * FROM PHONGCHIEU");
                                ViewBag.ListLcuaPC = data.getData("SELECT * FROM LOAIPC");
                            }
                            else
                            {
                                ViewBag.DSPC = data.getData("SELECT * FROM PHONGCHIEU");
                                ViewBag.ListLcuaPC = data.getData("SELECT * FROM LOAIPC");
                            }
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Lỗi không tồn tại loại phòng chiếu!";
                            ViewBag.DSPC = data.getData("SELECT * FROM PHONGCHIEU");
                            ViewBag.ListLcuaPC = data.getData("SELECT * FROM LOAIPC");
                        }
                    }

                }
                else if (status == "Update")
                {
                    if (TenPC != null && SoLuongGT > 0 && SoLuongGV > 0 && MaLPC != null)
                    {
                        bool isUpdate = data.updateRoom(MaPC, TenPC, SoLuongGT, SoLuongGV, MaLPC);
                        if (!isUpdate)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                            ViewBag.DSPC = data.getData("SELECT * FROM PHONGCHIEU");
                        }
                        else
                        {
                            ViewBag.DSPC = data.getData("SELECT * FROM PHONGCHIEU");
                        }
                    }
                }
                else
                {
                    ViewBag.DSPC = data.getData("SELECT * FROM PHONGCHIEU");
                    ViewBag.ListLcuaPC = data.getData("SELECT * FROM LOAIPC");
                }
            }

            return View();
        }



        public ActionResult RoomType()
        {
            SQLData data = new SQLData();
            if (data.getData("SELECT * FROM LOAIPC")!=null)
            {
                ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");
            }
            return View();
        }

        [HttpPost]
        public ActionResult RoomType(int? MaLPC, string TenLPC, string MoTaLPC, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (TenLPC != null && MoTaLPC != null)
                {
                    if (status == "Add")
                    {
                        bool isSaved = data.saveRoomType(TenLPC, MoTaLPC);
                        if (!isSaved)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                            ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");
                        }
                        else ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");
                    }
                    else if (status == "Update")
                    {
                        if (MaLPC != null)
                        {
                            bool isUpdate = data.updateRoomTypeDetail(MaLPC, TenLPC, MoTaLPC);
                            if (!isUpdate)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                                ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");
                            }
                            else ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");
                        }
                        else
                        {
                            ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                        }
                    }
                    else ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");

                } else ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");
            }
            else ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");

            return View();
        }

        public ActionResult TheLoaiVaPhim()
        {
            SQLData data = new SQLData();
            if (data.getData("SELECT * FROM TL_P") != null)
            {
                ViewBag.DSTLVP = data.getData("SELECT * FROM TL_P");
            }
            return View();
        }
        [HttpPost]
        public ActionResult TheLoaiVaPhim(int? MaTLP, int? MaPhim, int? MaTL, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (MaPhim != null && MaTL != null)
                    {
                        ArrayList listfilmtype = data.getData($"SELECT * FROM TL_P WHERE MaPhim={MaPhim} AND MaTL={MaTL}");
                        if (listfilmtype.Count == 0)
                        {
                            bool isSaved = data.saveTheLoaiVaPhim(MaPhim, MaTL);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                            }
                            else ViewBag.DSTLVP = data.getData("SELECT * FROM TL_P");
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Đã tồn tại!";
                            ViewBag.DSTLVP = data.getData("SELECT * FROM TL_P");
                        }

                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                        ViewBag.DSTLVP = data.getData("SELECT * FROM TL_P");
                    }

                }
                else if (status == "Update")
                {
                    if (MaPhim != null && MaTL != null)
                    {
                        bool isUpdate = data.updateTheLoaiVaPhim(MaTLP, MaPhim, MaTL);
                        if (!isUpdate)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                            ViewBag.DSTLVP = data.getData("SELECT * FROM TL_P");
                        }
                        else
                        {
                            ViewBag.DSTLVP = data.getData("SELECT * FROM TL_P");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                        ViewBag.DSTLVP = data.getData("SELECT * FROM TL_P");
                    }
                }
                else ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
            }

            return View();
        }

        public ActionResult KHType()
        {
            SQLData data = new SQLData();
            ViewBag.GetLKH = data.getData("SELECT* FROM LOAIKH");
            return View();
        }
        [HttpPost]
        public ActionResult KHType(int? MaLKH, string TenLKH, string CKLKH, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (TenLKH != null && CKLKH != null)
                {
                    if (status == "Add")
                    {
                        bool isSaved = data.saveKHType(TenLKH, CKLKH);
                        if (!isSaved)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                            ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
                        }
                        else ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
                    }
                    else if (status == "Update")
                    {
                        if (MaLKH != null)
                        {
                            bool isUpdate = data.updateKHType(MaLKH, TenLKH, CKLKH);
                            if (!isUpdate)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                                ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
                            }
                            else ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
                        }
                        else
                        {
                            ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                        }
                    }
                    else ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");

                }
                else ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
            }
            else ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");

            return View();
        }

        public ActionResult NVType()
        {
            SQLData data = new SQLData();
            ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");
            return View();
        }
        [HttpPost]
        public ActionResult NVType(int? MaLNV, string TenLNV, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (TenLNV != null)
                {
                    if (status == "Add")
                    {
                        bool isSaved = data.saveNVType(TenLNV);
                        if (!isSaved)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                            ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");
                        }
                        else ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");
                    }
                    else if (status == "Update")
                    {
                        if (MaLNV != null)
                        {
                            bool isUpdate = data.updateNVType(MaLNV, TenLNV);
                            if (!isUpdate)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                                ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");
                            }
                            else ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");
                        }
                        else
                        {
                            ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                        }
                    }
                    else ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");

                }
                else ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");
            }
            else ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");

            return View();
        }

        public ActionResult KhachHang()
        {
            SQLData data = new SQLData();
            ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
            ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
            return View();
        }
        [HttpPost]
        public ActionResult KhachHang(int? MaKH, string TenKH, string MatKhau, string Email, int? DiemThuong, string TrangThaiKH, string LoaiKH, int? MaLKHDetail, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenKH != null && MatKhau != null && Email != null && DiemThuong != null && TrangThaiKH != null && LoaiKH != null)
                    {
                        int getMaLKH = data.getMaLoaiKH(LoaiKH);
                        if (getMaLKH != 0)
                        {
                            bool isSaved = data.saveKH(TenKH, MatKhau, Email, DiemThuong, TrangThaiKH, getMaLKH);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công hoặc phòng chiếu đã tồn tại!";
                                ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
                                ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
                            }
                            else
                            {
                                ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
                                ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
                            }
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Lỗi không tồn tại loại khách hàng!";
                            ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
                            ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
                        }
                    }
                }
                else if (status == "Update")
                {
                    if (TenKH != null && MatKhau != null && Email != null && DiemThuong != null && TrangThaiKH != null && MaLKHDetail != null)
                    {
                        bool isUpdate = data.updateKH(MaKH, TenKH, MatKhau, Email, DiemThuong, TrangThaiKH, MaLKHDetail);
                        if (!isUpdate)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                            ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
                            ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
                        }
                        else
                        {
                            ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
                            ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi null dữ liệu!";
                        ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
                        ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
                    }

                }
                else
                {
                    ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
                    ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
                }
            }

            return View();
        }

        public ActionResult NhanVien()
        {
            SQLData data = new SQLData();
            ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
            ViewBag.GetListLNV = data.getData("SELECT TenLoaiNV FROM LAOINV");
            return View();
        }
        [HttpPost]
        public ActionResult NhanVien(int? MaNV, string TenNV, string MatKhauNV, string EmailNV, string TrangThaiNV, string LoaiNV, int? MaLNVDetail, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenNV != null && MatKhauNV != null && EmailNV != null && TrangThaiNV != null && LoaiNV != null)
                    {
                        int getMaLNV = data.getMaLoaiNV(LoaiNV);
                        if (getMaLNV != 0)
                        {
                            bool isSaved = data.saveNV(TenNV, MatKhauNV, EmailNV, TrangThaiNV, getMaLNV);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công hoặc đã tồn tại!";
                                ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                                ViewBag.GetListLNV = data.getData("SELECT TenLoaiNV FROM LAOINV");
                            }
                            else
                            {
                                ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                                ViewBag.GetListLNV = data.getData("SELECT TenLoaiNV FROM LAOINV");
                            }
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                            ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                            ViewBag.GetListLNV = data.getData("SELECT TenLoaiNV FROM LAOINV");
                        }
                    }

                }
                else if (status == "Update")
                {
                    if (TenNV != null && MatKhauNV != null && EmailNV != null && TrangThaiNV != null && MaLNVDetail != null)
                    {
                        bool isUpdate = data.updateNV(MaNV, TenNV, MatKhauNV, EmailNV, TrangThaiNV, MaLNVDetail);
                        if (!isUpdate)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                            ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                            ViewBag.GetListLNV = data.getData("SELECT TenLoaiNV FROM LAOINV");
                        }
                        else
                        {
                            ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                            ViewBag.GetListLNV = data.getData("SELECT TenLoaiNV FROM LAOINV");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi null dữ liệu!";
                        ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                        ViewBag.GetListLNV = data.getData("SELECT TenLoaiNV FROM LAOINV");
                    }

                }
                else
                {
                    ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                    ViewBag.GetListLNV = data.getData("SELECT TenLoaiNV FROM LAOINV");
                }
            }

            return View();
        }


        public ActionResult LoaiGhe()
        {
            SQLData data = new SQLData();
            ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
            return View();
        }
        [HttpPost]
        public ActionResult LoaiGhe(int? MaLG, string TenLG, int? GiaGhe, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (TenLG != null && GiaGhe != null)
                {
                    if (status == "Add")
                    {
                        bool isSaved = data.saveChairType(TenLG, GiaGhe);
                        if (!isSaved)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                            ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                        }
                        else ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                    }
                    else if (status == "Update")
                    {
                        if (MaLG != null)
                        {
                            bool isUpdate = data.updateChairType(MaLG, TenLG, GiaGhe);
                            if (!isUpdate)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                                ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                            }
                            else ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                        }
                        else
                        {
                            ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                        }
                    }
                    else ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");

                }
                else ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
            }
            else ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");

            return View();
        }

        public ActionResult XuatChieu()
        {
            SQLData data = new SQLData();
            ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
            return View();
        }
        [HttpPost]
        public ActionResult XuatChieu(int? MaXC, string CaXC, string GioXC, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (CaXC != null && GioXC != null)
                {
                    if (status == "Add")
                    {
                        bool isSaved = data.saveXC(CaXC, GioXC);
                        if (!isSaved)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                            ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
                        }
                        else ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
                    }
                    else if (status == "Update")
                    {
                        if (CaXC != null && GioXC != null && MaXC != null)
                        {
                            bool isUpdate = data.updateXC(MaXC, CaXC, GioXC);
                            if (!isUpdate)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                                ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
                            }
                            else ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
                        }
                        else
                        {
                            ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                        }
                    }
                    else ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");

                }
                else ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
            }
            else ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");

            return View();
        }

        public ActionResult LichChieu()
        {
            SQLData data = new SQLData();
            ViewBag.GetXC = data.getData("SELECT CaXC FROM XUATCHIEU");
            ViewBag.GetPC = data.getData("SELECT TenPC FROM PHONGCHIEU");
            ViewBag.GetPhim = data.getData("SELECT TenPhim FROM PHIM");
            ViewBag.GetLC = data.getData("SELECT * FROM LICHCHIEU");
            return View();
        }
        [HttpPost]
        public ActionResult LichChieu(int? MaLC, string NgayLC, string TrangThaiLC, int? SLVeDat, string CaXC, string MaPC, string MaPhim, int? CaXCDetail, int? MaPhimDetail, int? MaPCDetail, string status)
        {
            SQLData data = new SQLData();

            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (NgayLC != null && TrangThaiLC != null && SLVeDat != null && CaXC != null && MaPhim != null && MaPC != null)
                    {
                        int getMaPC = data.getMaPC(MaPC);
                        int getMaPhim = data.getMaPhim(MaPhim);
                        int getMaXC = data.getMaXC(CaXC);

                        if (getMaPC != 0 && getMaPhim != 0 && getMaXC != 0)
                        {
                            bool isSaved = data.saveLC(NgayLC, TrangThaiLC, SLVeDat, getMaXC, getMaPC, getMaPhim);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công hoặc đã tồn tại!";
                                ViewBag.GetXC = data.getData("SELECT CaXC FROM XUATCHIEU");
                                ViewBag.GetPC = data.getData("SELECT TenPC FROM PHONGCHIEU");
                                ViewBag.GetPhim = data.getData("SELECT TenPhim FROM PHIM");
                                ViewBag.GetLC = data.getData("SELECT * FROM LICHCHIEU");
                            }
                            else
                            {
                                ViewBag.GetXC = data.getData("SELECT CaXC FROM XUATCHIEU");
                                ViewBag.GetPC = data.getData("SELECT TenPC FROM PHONGCHIEU");
                                ViewBag.GetPhim = data.getData("SELECT TenPhim FROM PHIM");
                                ViewBag.GetLC = data.getData("SELECT * FROM LICHCHIEU");
                            }
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Lỗi không tồn tại!";
                            ViewBag.GetXC = data.getData("SELECT CaXC FROM XUATCHIEU");
                            ViewBag.GetPC = data.getData("SELECT TenPC FROM PHONGCHIEU");
                            ViewBag.GetPhim = data.getData("SELECT TenPhim FROM PHIM");
                            ViewBag.GetLC = data.getData("SELECT * FROM LICHCHIEU");
                        }
                    }

                }
                else if (status == "Update")
                {
                    if (NgayLC != null && TrangThaiLC != null && SLVeDat != null && CaXCDetail != null && MaPhimDetail != null && MaPCDetail != null)
                    {
                        bool isUpdate = data.updateLC(MaLC, NgayLC, TrangThaiLC, SLVeDat, CaXCDetail, MaPhimDetail, MaPCDetail);
                        if (!isUpdate)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                            ViewBag.GetXC = data.getData("SELECT CaXC FROM XUATCHIEU");
                            ViewBag.GetPC = data.getData("SELECT TenPC FROM PHONGCHIEU");
                            ViewBag.GetPhim = data.getData("SELECT TenPhim FROM PHIM");
                            ViewBag.GetLC = data.getData("SELECT * FROM LICHCHIEU");
                        }
                        else
                        {
                            ViewBag.GetXC = data.getData("SELECT CaXC FROM XUATCHIEU");
                            ViewBag.GetPC = data.getData("SELECT TenPC FROM PHONGCHIEU");
                            ViewBag.GetPhim = data.getData("SELECT TenPhim FROM PHIM");
                            ViewBag.GetLC = data.getData("SELECT * FROM LICHCHIEU");
                        }
                    }
                }
                else
                {
                    ViewBag.GetXC = data.getData("SELECT CaXC FROM XUATCHIEU");
                    ViewBag.GetPC = data.getData("SELECT TenPC FROM PHONGCHIEU");
                    ViewBag.GetPhim = data.getData("SELECT TenPhim FROM PHIM");
                    ViewBag.GetLC = data.getData("SELECT * FROM LICHCHIEU");
                }
            }

            return View();
        }

        public ActionResult ReportHD(string status)
        {
            SQLData data = new SQLData();

            ViewBag.GetHD = data.getData("SELECT * FROM HOADON");

            ViewBag.GetTenP = data.getData("SELECT p.TenPhim " +
                                            "FROM HOADON hd, CHITIETHD cthd, VEPHIM vp, LICHCHIEU lc, PHIM p " +
                                            "WHERE cthd.MaHD = hd.MaHD " +
                                            "AND cthd.MaVe = vp.MaVe " +
                                            "AND vp.MaLC = lc.MaLC " +
                                            "AND p.MaPhim = lc.MaPhim " +
                                            "GROUP BY p.TenPhim");
            ViewBag.GetSLHDP = data.getData("SELECT COUNT(p.TenPhim) as SLP " +
                                            "FROM HOADON hd, CHITIETHD cthd, VEPHIM vp, LICHCHIEU lc, PHIM p " +
                                            "WHERE cthd.MaHD = hd.MaHD " +
                                            "AND cthd.MaVe = vp.MaVe " +
                                            "AND vp.MaLC = lc.MaLC " +
                                            "AND p.MaPhim = lc.MaPhim " +
                                            "GROUP BY p.TenPhim");

            ViewBag.GetTenKH = data.getData("SELECT kh.TenTKKH FROM HOADON hd, KHACHHANG kh WHERE hd.MaKH = kh.MaKH GROUP BY kh.TenTKKH");
            ViewBag.GetSLHDKH = data.getData("SELECT COUNT(*) as SLHD FROM HOADON hd, KHACHHANG kh WHERE hd.MaKH = kh.MaKH GROUP BY kh.TenTKKH");

            // Trong controller
            ArrayList dataPoints1 = new ArrayList();
            ArrayList dataPoints2 = new ArrayList();

            for (int i = 0; i < ViewBag.GetSLHDP.Count; i++)
            {
                dataPoints1.Add(new object[] { ViewBag.GetTenP[i][0], ViewBag.GetSLHDP[i][0] });
            }

            for (int i = 0; i < ViewBag.GetSLHDKH.Count; i++)
            {
                dataPoints2.Add(new object[] { ViewBag.GetTenKH[i][0], ViewBag.GetSLHDKH[i][0] });
            }

            if (dataPoints1 != null)
            {
                ViewBag.DataPoints1 = dataPoints1;
            }
            if (dataPoints2 != null)
            {
                ViewBag.DataPoints2 = dataPoints2;
            }

            if (status == "Detail")
            {
                return RedirectToAction("HDDetail", "Admin");
            }


            return View();
        }

        public ActionResult HDDetail(int? MaHD)
        {
            SQLData data = new SQLData();
            ViewBag.GetCTHD = data.getData($"select cthd.* from HOADON hd, CHITIETHD cthd where hd.MaHD={MaHD} and hd.MaHD=cthd.MaHD");
            ViewBag.GetCTVP = data.getData($"select vp.* from HOADON hd, CHITIETHD cthd, VEPHIM vp where hd.MaHD={MaHD} and hd.MaHD=cthd.MaHD and vp.MaVe=cthd.MaVe");
            return View();
        }
    }
}