using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections; // Sử dụng Lớp ArrayList để lưu kết quả
using System.Data.SqlClient;// Sử dụng các lớp tương tác CSDL
using CNPMNC_REPORT1.Models;
using System.IO;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.Factory;
using CNPMNC_REPORT1.Factory.FactoryLoaiPhim;
using CNPMNC_REPORT1.Factory.FactoryPhim;
using CNPMNC_REPORT1.Factory.FactoryGHT;

namespace CNPMNC_REPORT1.Areas.AdminArea.Controllers
{
    public class AdminController : Controller
    {
        private string LogError = "";


        public ActionResult Film()
        {
            LoaiPhimFactory loaiPhimFactory = new CreateAllLP();
            PhimFactory phimFactory = new CreateAllPhim();
            GHTFactory ghtFactory = new CreateAllGHT();

            ViewBag.DSGHTP = ghtFactory.CreateGHT();
            ViewBag.DSLPP = loaiPhimFactory.CreateLoaiP();
            ViewBag.DSF = phimFactory.CreatePhim();
            ViewBag.ThongBao = LogError;

            return View();
        }

        [HttpPost]
        public ActionResult Film(string MaP, string TenF, string MoTaF, string NgayCC, string ThoiLuongP, string HinhAnhP, string TrailerP, string GHTP, string GiaP, string MaGHT, HttpPostedFileBase HinhAnhFile, HttpPostedFileBase HinhAnhFiledetail1, string HinhAnhFiledetail2, string status)
        {
            SQLPhim sqlPhim = new SQLPhim();
            SQLGioiHanTuoi sqlGHT = new SQLGioiHanTuoi();

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

                            string maGHT = sqlGHT.ChuyenTen_Ma(GHTP);

                            if (maGHT != "")
                            {
                                Phim phim = new Phim(TenF, MoTaF, NgayCC, ThoiLuongP, "0", "0", fileName, TrailerP, GiaP, maGHT);
                                bool isSaved = sqlPhim.ThemPhim(phim);
                                if (!isSaved)
                                {
                                    LogError = "Lỗi lưu không thành công hoặc phim đã tồn tại!";
                                    return RedirectToAction("Film", "Admin");
                                }
                                else
                                {
                                    return RedirectToAction("Film", "Admin");
                                }
                            }
                            else
                            {
                                LogError = "Lỗi không tồn tại giới hạn tuổi!";
                                return RedirectToAction("Film", "Admin");
                            }
                        }
                        else
                        {
                            LogError = "Lỗi không tồn tại hình ảnh!";
                            return RedirectToAction("Film", "Admin");
                        }

                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi null dữ liệu!";
                        return RedirectToAction("Film", "Admin");
                    }
                }
                else if (status == "Update")
                {
                    PhimFactory phimFactory = new CreateAllPhim();
                    List<Phim> dsPhim = phimFactory.CreatePhim();

                    if (TenF != null && MoTaF != null && NgayCC != null && TrailerP != null && ThoiLuongP != null && MaGHT != null && GiaP != null)
                    {
                        if (HinhAnhFiledetail1 != null)
                        {
                            List<string> changedFields = new List<string>();

                            int maPhim = dsPhim.FindIndex(x => x.MaPhim == MaP);

                            if (maPhim != -1)
                            {
                                Phim phim = dsPhim[maPhim];

                                var fileName1 = Path.GetFileName(HinhAnhFiledetail1.FileName);
                                var path1 = Path.Combine(Server.MapPath("~/img_phim"), fileName1);
                                HinhAnhFiledetail1.SaveAs(path1);

                                if (phim.TenPhim != TenF)
                                {
                                    changedFields.Add("TenPhim");
                                }
                                if (phim.TomTatP != MoTaF)
                                {
                                    changedFields.Add("TomTatP");
                                }
                                if (phim.NgayCongChieu != NgayCC)
                                {
                                    changedFields.Add("NgayCongChieu");
                                }
                                if (phim.ThoiLuongP != ThoiLuongP)
                                {
                                    changedFields.Add("ThoiLuongP");
                                }
                                if (phim.HinhAnh != fileName1)
                                {
                                    changedFields.Add("HinhAnh");
                                }
                                if (phim.Trailer != TrailerP)
                                {
                                    changedFields.Add("Trailer");
                                }
                                if (phim.GiaPhim != GiaP)
                                {
                                    changedFields.Add("GiaPhim");
                                }
                                if (phim.MaGHT != MaGHT)
                                {
                                    changedFields.Add("MaGHT");
                                }

                                // Cập nhật dữ liệu cho phim
                                bool updateResult = false;

                                foreach (var field in changedFields)
                                {
                                    // Dựa vào tên cột đã thay đổi (field) để cập nhật dữ liệu cho phim
                                    switch (field)
                                    {
                                        case "TenPhim":
                                            phim.TenPhim = TenF;
                                            updateResult = sqlPhim.CapNhatP(MaP, "TenPhim", phim.TenPhim);
                                            break;
                                        case "TomTatP":
                                            phim.TomTatP = MoTaF;
                                            updateResult = sqlPhim.CapNhatP(MaP, "TomTatP", phim.TomTatP);
                                            break;
                                        case "NgayCongChieu":
                                            phim.NgayCongChieu = NgayCC;
                                            updateResult = sqlPhim.CapNhatP(MaP, "NgayCongChieu", phim.NgayCongChieu);
                                            break;
                                        case "ThoiLuongP":
                                            phim.ThoiLuongP = ThoiLuongP;
                                            updateResult = sqlPhim.CapNhatP(MaP, "ThoiLuongP", phim.ThoiLuongP);
                                            break;
                                        case "HinhAnh":
                                            phim.HinhAnh = fileName1;
                                            updateResult = sqlPhim.CapNhatP(MaP, "HinhAnh", phim.HinhAnh);
                                            break;
                                        case "Trailer":
                                            phim.Trailer = TrailerP;
                                            updateResult = sqlPhim.CapNhatP(MaP, "Trailer", phim.Trailer);
                                            break;
                                        case "GiaPhim":
                                            phim.GiaPhim = GiaP;
                                            updateResult = sqlPhim.CapNhatP(MaP, "GiaPhim", phim.GiaPhim);
                                            break;
                                        case "MaGHT":
                                            phim.MaGHT = MaGHT;
                                            updateResult = sqlPhim.CapNhatP(MaP, "MaGHT", phim.MaGHT);
                                            break;
                                        default:
                                            LogError = "Không có thay đổi dữ liệu!";
                                            break;
                                    }
                                }

                                if (updateResult)
                                {
                                    // Cập nhật thành công
                                    return RedirectToAction("Film", "Admin");
                                }
                                else
                                {
                                    // Cập nhật thất bại
                                    LogError = "Cập nhật thất bại!";
                                    return RedirectToAction("Film", "Admin");
                                }

                            }
                        }
                        else
                        {
                            LogError = "null image!";
                            return RedirectToAction("Film", "Admin");
                        }
                        
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = $"Lỗi null {MaP},{TenF},{MoTaF},{NgayCC},{ThoiLuongP},{TrailerP},{GiaP},{MaGHT}!";
                    }
                }
                else ViewBag.ThongBaoLuu = "Lỗi model!";
            }
            return View();
        }

        public ActionResult FilmType()
        {
            SQLData123 data = new SQLData123();
            ViewBag.DSTLF = data.getData("SELECT * FROM THELOAIP");
            return View();
        }
        [HttpPost]
        public ActionResult FilmType(int? MaTL,string TenLF, string MoTaLF, string status)
        {
            SQLData123 data = new SQLData123();
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
            SQLData123 data = new SQLData123();
            if (data.getData("SELECT * FROM GIOIHANTUOI")!=null)
            {
                ViewBag.DSGHT = data.getData("SELECT * FROM GIOIHANTUOI");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AgeLimit(int? MaGHT,string TenGHT,string MoTaGHT, string status)
        {
            SQLData123 data = new SQLData123();
            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenGHT != null && MoTaGHT != null)
                    {
                        ArrayList listfilmtype = data.getData($"SELECT * FROM GIOIHANTUOI WHERE TenGHT=N'{TenGHT}'");
                        if (listfilmtype.Count == 0)
                        {
                            bool isSaved = data.saveAgeLimit(TenGHT.Trim(), MoTaGHT);
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
                        bool isUpdate = data.updateAgeLimit(MaGHT, TenGHT.Trim(), MoTaGHT);
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
            SQLData123 data = new SQLData123();
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
            SQLData123 data = new SQLData123();

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
            SQLData123 data = new SQLData123();
            if (data.getData("SELECT * FROM LOAIPC")!=null)
            {
                ViewBag.DSLPC = data.getData("SELECT * FROM LOAIPC");
            }
            return View();
        }

        [HttpPost]
        public ActionResult RoomType(int? MaLPC, string TenLPC, string MoTaLPC, string status)
        {
            SQLData123 data = new SQLData123();

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
            SQLData123 data = new SQLData123();
            if (data.getData("SELECT * FROM TL_P") != null)
            {
                ViewBag.DSTLVP = data.getData("SELECT * FROM TL_P");
            }
            return View();
        }
        [HttpPost]
        public ActionResult TheLoaiVaPhim(int? MaTLP, int? MaPhim, int? MaTL, string status)
        {
            SQLData123 data = new SQLData123();

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
            SQLData123 data = new SQLData123();
            ViewBag.GetLKH = data.getData("SELECT* FROM LOAIKH");
            return View();
        }
        [HttpPost]
        public ActionResult KHType(int? MaLKH, string TenLKH, double CKLKH, string status)
        {
            SQLData123 data = new SQLData123();

            if (ModelState.IsValid)
            {
                if (TenLKH != null && CKLKH != null)
                {
                    if (status == "Add")
                    {
                        bool isCheckName = data.checkName("TenLKH", "LOAIKH", TenLKH.Trim());
                        if (isCheckName)
                        {
                            bool isSaved = data.saveKHType(TenLKH, CKLKH/100);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                                ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
                            }
                            else ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Trùng tên loại!";
                            ViewBag.GetLKH = data.getData("SELECT * FROM LOAIKH");
                        }
                    }
                    else if (status == "Update")
                    {
                        if (MaLKH != null)
                        {
                            bool isUpdate = data.updateKHType(MaLKH, TenLKH, CKLKH/100);
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
            SQLData123 data = new SQLData123();
            ViewBag.GetLNV = data.getData("SELECT * FROM LAOINV");
            return View();
        }
        [HttpPost]
        public ActionResult NVType(int? MaLNV, string TenLNV, string status)
        {
            SQLData123 data = new SQLData123();

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
            SQLData123 data = new SQLData123();
            ViewBag.GetKH = data.getData("SELECT * FROM KHACHHANG");
            ViewBag.GetListLKH = data.getData("SELECT TenLKH FROM LOAIKH");
            return View();
        }
        [HttpPost]
        public ActionResult KhachHang(int? MaKH, string TenKH, string MatKhau, string Email, int? DiemThuong, string TrangThaiKH, string LoaiKH, int? MaLKHDetail, string status)
        {
            SQLData123 data = new SQLData123();

            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenKH != null && MatKhau != null && Email != null && DiemThuong != null && TrangThaiKH != null && LoaiKH != null)
                    {
                        int getMaLKH = data.getMaLoaiKH(LoaiKH);
                        if (getMaLKH != 0)
                        {
                            bool isCheck = data.checkDataUsername(TenKH);
                            if (isCheck && MatKhau.Length > 8)
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
                                ViewBag.ThongBaoLuu = "Lỗi trùng tên hoặc mật khẩu ngắn hơn 8!";
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
            SQLData123 data = new SQLData123();
            ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
            return View();
        }
        [HttpPost]
        public ActionResult NhanVien(int? MaNV, string TenNV, string MatKhauNV, string EmailNV, string TrangThaiNV, string status)
        {
            SQLData123 data = new SQLData123();

            if (ModelState.IsValid)
            {
                if (status == "Add")
                {
                    if (TenNV != null && MatKhauNV != null && EmailNV != null && TrangThaiNV != null)
                    {
                        bool isCheckEmail = data.checkName("Email", "NHANVIEN", EmailNV.Trim());

                        if (isCheckEmail && MatKhauNV.Length>8)
                        {
                            bool isSaved = data.saveNV(TenNV, MatKhauNV, EmailNV, TrangThaiNV);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công hoặc đã tồn tại!";
                                ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                            }
                            else
                            {
                                ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                            }
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Lỗi trùng Email hoặc mật khẩu không hợp lệ!";
                            ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                        }

                    }

                }
                else if (status == "Update")
                {
                    if (TenNV != null && MatKhauNV != null && EmailNV != null && TrangThaiNV != null)
                    {
                        bool isUpdate = data.updateNV(MaNV, TenNV, MatKhauNV, EmailNV, TrangThaiNV);
                        if (!isUpdate)
                        {
                            ViewBag.ThongBaoLuu = "Lỗi cập nhật không thành công!";
                            ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                        }
                        else
                        {
                            ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                        }
                    }
                    else
                    {
                        ViewBag.ThongBaoLuu = "Lỗi null dữ liệu!";
                        ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                    }

                }
                else
                {
                    ViewBag.GetNV = data.getData("SELECT * FROM NHANVIEN");
                }
            }

            return View();
        }


        public ActionResult LoaiGhe()
        {
            SQLData123 data = new SQLData123();
            ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
            return View();
        }
        [HttpPost]
        public ActionResult LoaiGhe(int? MaLG, string TenLG, int? GiaGhe, string status)
        {
            SQLData123 data = new SQLData123();

            if (ModelState.IsValid)
            {
                if (TenLG != null && GiaGhe != null)
                {
                    if (status == "Add")
                    {
                        bool isCheckName = data.checkName("TenLG", "LOAIGHE", TenLG.Trim());
                        if (isCheckName)
                        {
                            bool isSaved = data.saveChairType(TenLG, GiaGhe);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                                ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                            }
                            else ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Trùng tên ghế!";
                            ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                        }
                    }
                    else if (status == "Update")
                    {
                        if (MaLG != null)
                        {
                            bool isUpdate = data.updateChairType(MaLG, TenLG.Trim(), GiaGhe);
                            if (!isUpdate)
                            {
                                ViewBag.ThongBaoLuu = "Trùng tên ghế!";
                                ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                            }
                            else
                            {
                                ViewBag.GetLG = data.getData("SELECT* FROM LOAIGHE");
                            }
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
            SQLData123 data = new SQLData123();
            ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
            return View();
        }
        [HttpPost]
        public ActionResult XuatChieu(int? MaXC, string CaXC, string GioXC, string status)
        {
            SQLData123 data = new SQLData123();

            if (ModelState.IsValid)
            {
                if (CaXC != null && GioXC != null)
                {
                    if (status == "Add")
                    {
                        bool isCheckName = data.checkName("CaXC", "XUATCHIEU", CaXC.Trim());
                        if (isCheckName)
                        {
                            bool isSaved = data.saveXC(CaXC.Trim(), GioXC);
                            if (!isSaved)
                            {
                                ViewBag.ThongBaoLuu = "Lỗi lưu không thành công!";
                                ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
                            }
                            else ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
                        }
                        else
                        {
                            ViewBag.ThongBaoLuu = "Lỗi trùng ca chiếu!";
                            ViewBag.GetXC = data.getData("SELECT* FROM XUATCHIEU");
                        }
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
            SQLData123 data = new SQLData123();
            ViewBag.GetXC = data.getData("SELECT CaXC FROM XUATCHIEU");
            ViewBag.GetPC = data.getData("SELECT TenPC FROM PHONGCHIEU");
            ViewBag.GetPhim = data.getData("SELECT TenPhim FROM PHIM");
            ViewBag.GetLC = data.getData("SELECT * FROM LICHCHIEU");
            return View();
        }
        [HttpPost]
        public ActionResult LichChieu(int? MaLC, string NgayLC, string TrangThaiLC, int? SLVeDat, string CaXC, string MaPC, string MaPhim, int? CaXCDetail, int? MaPhimDetail, int? MaPCDetail, string status)
        {
            SQLData123 data = new SQLData123();

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
            SQLData123 data = new SQLData123();

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


            return View();
        }

        public ActionResult HDDetail(int? MaHD)
        {
            SQLData123 data = new SQLData123();
            ViewBag.GetCTHD = data.getData($"select cthd.* from HOADON hd, CHITIETHD cthd where hd.MaHD={MaHD} and hd.MaHD=cthd.MaHD");
            ViewBag.GetCTVP = data.getData($"select vp.NgayDat, vp.GiaVe, lc.NgayLC, p.TenPhim, vp.MaVe from HOADON hd, CHITIETHD cthd, VEPHIM vp, LICHCHIEU lc, PHIM p where hd.MaHD={MaHD} and hd.MaHD=cthd.MaHD and vp.MaVe=cthd.MaVe AND vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim");

            
            ViewBag.GetTenGhe = data.getData($"select vg.TenGheVG, vg.MaVe from VEPHIM vp, HOADON hd, VE_GHE vg, CHITIETHD cthd where hd.MaHD={MaHD} and hd.MaHD=cthd.MaHD and vp.MaVe=cthd.MaVe and vp.MaVe=vg.MaVe");

            return View();
        }
    }
}