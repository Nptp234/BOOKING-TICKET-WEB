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

namespace CNPMNC_REPORT1.Controllers
{
    public class StoreController : Controller
    {
        SQLData data = new SQLData();
        // GET: Store
        public ActionResult Index()
        {
            string tentk = Session["Username"].ToString();
            ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
            ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");


            return View();
        }
        public ActionResult IndexForNull()
        {
            string tentk = Session["Username"].ToString();
            ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
            ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
            ArrayList list = ViewBag.GetDSVP;
            if (list.Count != 0)
            {
                return RedirectToAction("Index", "Store");
            }
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

            if (status == "Delete")
            {
                string tentk = Session["Username"].ToString();
                if (MaVe != null)
                {
                    bool isDelete = data.deleteVP(MaVe);
                    if (isDelete)
                    {
                        ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                        ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");

                    }
                }
            }
            else if (status == "ThanhToan")
            {
                string tentk = Session["Username"].ToString();
                if (TongGia != null && (ThanhTien!=null||ThanhTien!=0))
                {
                    bool isAdd = data.insertHD(TongGia, tentk);
                    if (isAdd)
                    {
                        bool isAddCT = false;
                        ViewBag.GetMaVP = data.getData($"SELECT vp.MaVe FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH=kh.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                        //ViewBag.GetMaVeHD = data.getData($"SELECT MAX(MaHD) FROM HOADON");

                        foreach (var b in ViewBag.GetMaVP)
                        {
                            int mave = int.Parse(b[0]);
                            //ViewBag.GetSLVP = data.getData($"SELECT COUNT(MaVG) FROM VE_GHE WHERE MaVe = {mave} AND TrangThaiVG = N'CHƯA THANH TOÁN'");
                            //int slvp = int.Parse(ViewBag.GetSLVP[0][0]);
                            isAddCT = data.insertCTHD(mave, 1, ThanhTien);

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

        
    }
}