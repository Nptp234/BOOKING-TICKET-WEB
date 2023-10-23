using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPMNC_REPORT1.Models;
namespace CNPMNC_REPORT1.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        SQLData db = new SQLData();
        //public ActionResult LichChieu(string idPhim)
        //{
        //    db.getData("SELECT* FROM PHIM WHERE ");
        //    return View();
        //}
        public ActionResult LichChieu(string maphim = "1", string ngay = "")
        {
            
            if (ngay == "")
            {
                ngay = DateTime.Now.ToString("yyyy-MM-dd");
            }
            ViewBag.Phim = db.getData($"SELECT PHIM.*, GIOIHANTUOI.MoTaGHT, GIOIHANTUOI.TenGHT FROM PHIM, GIOIHANTUOI WHERE MaPhim = {maphim} AND GIOIHANTUOI.MaGHT = PHIM.MaGHT") ;
            //ViewBag.ListShowTime = db.getData($"SELECT* FROM PHIM WHERE MaPhim = {maphim}");
            ViewBag.ListType = db.getData($"SELECT THELOAIP.MoTaTL, THELOAIP.TenTL FROM THELOAIP, TL_P WHERE THELOAIP.MaTL = TL_P.MaTL AND TL_P.MaPhim = {maphim}");
            ViewBag.List2D = db.getData($"SELECT DISTINCT XUATCHIEU.GioXC FROM LICHCHIEU, PHONGCHIEU, LOAIPC, XUATCHIEU WHERE MaPhim = {maphim} AND LOAIPC.TenLPC = '2D' AND XUATCHIEU.MaXC = LICHCHIEU.CaXC AND LICHCHIEU.MaPC = PHONGCHIEU.MaPC AND PHONGCHIEU.MaLPC = LOAIPC.MaLPC AND LICHCHIEU.NgayLC = '{ngay}' ORDER BY GioXC ASC ");
            ViewBag.List3D = db.getData($"SELECT DISTINCT XUATCHIEU.GioXC FROM LICHCHIEU, PHONGCHIEU, LOAIPC, XUATCHIEU WHERE MaPhim = {maphim} AND LOAIPC.TenLPC = '3D' AND XUATCHIEU.MaXC = LICHCHIEU.CaXC AND LICHCHIEU.MaPC = PHONGCHIEU.MaPC AND PHONGCHIEU.MaLPC = LOAIPC.MaLPC AND LICHCHIEU.NgayLC = '{ngay}' ORDER BY GioXC ASC ");

            string input = ViewBag.Phim[0][8];
            string delimiter = "https://www.youtube.com/watch?v=";
            string[] tokens = input.Split(new string[] { delimiter }, StringSplitOptions.None);
            if (tokens.Count() > 0)
            {
                ViewBag.GetEmbedLink = "https://www.youtube.com/embed/" + tokens[1].Trim();
            }
            return View();
        }

        public JsonResult ChangeDay(string maphim, string date)
        {
            ArrayList list2D = db.getData($"SELECT DISTINCT XUATCHIEU.GioXC FROM LICHCHIEU, PHONGCHIEU, LOAIPC, XUATCHIEU WHERE MaPhim = {maphim} AND LOAIPC.TenLPC = '2D' AND XUATCHIEU.MaXC = LICHCHIEU.CaXC AND LICHCHIEU.MaPC = PHONGCHIEU.MaPC AND PHONGCHIEU.MaLPC = LOAIPC.MaLPC AND LICHCHIEU.NgayLC = '{date}' ORDER BY GioXC ASC");
            ArrayList list3D = db.getData($"SELECT DISTINCT XUATCHIEU.GioXC FROM LICHCHIEU, PHONGCHIEU, LOAIPC, XUATCHIEU WHERE MaPhim = {maphim} AND LOAIPC.TenLPC = '3D' AND XUATCHIEU.MaXC = LICHCHIEU.CaXC AND LICHCHIEU.MaPC = PHONGCHIEU.MaPC AND PHONGCHIEU.MaLPC = LOAIPC.MaLPC AND LICHCHIEU.NgayLC = '{date}' ORDER BY GioXC ASC");
            //var data = new { Check = true, Email = email, Name = name, Address = address };
            var data = new { List2D = list2D, List3D = list3D };
            return Json(data);
        }

        public ActionResult ChonGhe()
        {
            
            return View();
        }
    }
}