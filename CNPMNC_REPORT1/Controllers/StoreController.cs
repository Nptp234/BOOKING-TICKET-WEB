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
using CNPMNC_REPORT1.Command;
using CNPMNC_REPORT1.Command.CommandDV;

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

            if (ViewBag.GetDSVP.Count == 0)
            {
                return RedirectToAction("IndexForNull", "Store");
            }

            return View();
        }
        public ActionResult IndexForNull()
        {

            return View();
        }

        public ActionResult ThanhToanVeP(string MaVe, string TongGia, string ThanhTien)
        {
            CommandInterface command = new ThanhToanVeCommand(MaVe, TongGia, ThanhTien);
            InvokerClass invoker = new InvokerClass();
            invoker.SetCommand(command);
            invoker.ExecuteCommand();

            return RedirectToAction("ThankYouPage", "Booking");
        }

        public ActionResult XoaVe(string maVe)
        {
            CommandInterface command = new HuyVeCommand(maVe);
            InvokerClass invoker = new InvokerClass();
            invoker.SetCommand(command);
            invoker.ExecuteCommand();

            return RedirectToAction("KiemTraChuyenTrang", "Store");
        }
    }
}