using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CNPMNC_REPORT1.Models;
using System.Net;
using System.Net.Mail;
using System.Collections;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.SQLFolder;

namespace CNPMNC_REPORT1.Controllers
{
    public class StoreController : Controller
    {
        SQLData123 data = new SQLData123();
        SQLUser user = SQLUser.Instance;
        SQLVePhim veP = new SQLVePhim();

        public ActionResult KiemTraChuyenTrang()
        {
            string tentk = Session["Username"].ToString();
            ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
            ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

            ArrayList list = ViewBag.GetDSVP;
            if (list.Count != 0)
            {
                return RedirectToAction("Index", "Store");
            }
            else
            {
                return RedirectToAction("IndexForNull", "Store");
            }
        }


        public ActionResult Index()
        {
            string tentk = "";
            if (user.KH != null)
            {
                tentk = user.KH.TenTKKH;
            }

            ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
            ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");


            return View();
        }
        public ActionResult IndexForNull()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Index(string status, int? MaVe, int? TongGia, int? ThanhTien)
        {
            if (Session["Username"] != null)
            {
                string tentk = Session["Username"].ToString();
                ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

            } 
            if (status == "ThanhToan")
            {
                string tentk = Session["Username"].ToString();
                if (TongGia != null && (ThanhTien!=null||ThanhTien!=0))
                {
                    bool isAdd = data.insertHD(TongGia, tentk);
                    if (isAdd)
                    {
                        bool isAddCT = false, isUpdateLM = false;
                        ViewBag.GetMaVP = data.getData($"SELECT vp.MaVe, p.MaPhim FROM VEPHIM vp, KHACHHANG kh, LICHCHIEU lc, PHIM p WHERE vp.MaKH=kh.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN' AND vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim");
                        //ViewBag.GetMaVeHD = data.getData($"SELECT MAX(MaHD) FROM HOADON");

                        foreach (var b in ViewBag.GetMaVP)
                        {
                            int mave = int.Parse(b[0]);
                            int maphim = int.Parse(b[1]);
                            //ViewBag.GetSLVP = data.getData($"SELECT COUNT(MaVG) FROM VE_GHE WHERE MaVe = {mave} AND TrangThaiVG = N'CHƯA THANH TOÁN'");
                            //int slvp = int.Parse(ViewBag.GetSLVP[0][0]);
                            isAddCT = data.insertCTHD(mave, 1, ThanhTien);
                            isUpdateLM = data.updateLuotMua(maphim);
                        }

                        if (isAddCT)
                        {
                            bool isUpdate = false;
                            foreach (var b in ViewBag.GetMaVP)
                            {
                                int mave = int.Parse(b[0]);
                                isUpdate = data.updateTTVP(mave);
                            }
                            if (isUpdate)
                            {
                                bool isUpdateVG = false;
                                foreach (var b in ViewBag.GetMaVP)
                                {
                                    int mave = int.Parse(b[0]);
                                    ViewBag.GetVeGhe = data.getData($"SELECT * FROM VE_GHE WHERE MaVe = {mave} AND TrangThaiVG = N'CHƯA THANH TOÁN'");
                                    foreach (var b1 in ViewBag.GetVeGhe)
                                    {
                                        isUpdateVG = data.updateTTVG(mave);
                                    }
                                }
                                if (isUpdateVG)
                                {
                                    ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                                    ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                                    return RedirectToAction("ThankYouPage", "Booking");
                                }
                            }
                        }
                        else
                        {
                            ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                            ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                            ViewBag.ThongBao = "Error add CTHD!";
                        }
                    }
                    else
                    {
                        ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                        ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                        ViewBag.ThongBao = "Error add HD!";
                    }
                }
                else
                {
                    ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                    ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                }

            }

            return View();
        }

        public ActionResult XoaVe(string maVe)
        {
            bool isDelete = veP.KiemTraXoaVe(maVe);
            if (!isDelete)
            {
                TempData["ThongBao"] = "Lỗi xóa!";
            }
            return RedirectToAction("KiemTraChuyenTrang", "Store");
        }
    }
}