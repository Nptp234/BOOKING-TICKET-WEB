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
            if (Session["Username"] != null)
            {
                string tentk = Session["Username"].ToString();
                ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                ViewBag.GetCountVP = data.getData($"SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = '{tentk}' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
                   
            }


            return View();
        }
        [HttpPost]
        public ActionResult Index(string status, int? MaVe)
        {
            if (Session["Username"] != null)
            {
                string tentk = Session["Username"].ToString();
                ViewBag.GetDSVP = data.getData($"SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'");
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
                    }
                }
            }

            return View();
        }
    }
}