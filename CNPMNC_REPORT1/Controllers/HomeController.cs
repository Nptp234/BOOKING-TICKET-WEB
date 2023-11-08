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
using Unidecode.NET;

namespace CNPMNC_REPORT1.Controllers
{
    public class HomeController : Controller
    {

        SQLData db = new SQLData();
        public ActionResult Index(string Logout)
        {
            //Lấy danh sách phim đang khởi chiếu
            ViewBag.NowShowing = db.getData("SELECT * FROM PHIM WHERE NgayCongChieu <= GETDATE()");
            //Lấy danh sách phim sắp khởi chiếu
            ViewBag.ComingSoon = db.getData("SELECT * FROM PHIM WHERE NgayCongChieu > GETDATE()");
            //Lấy danh sách phim xem nhiều nhất
            ViewBag.MostWatching = db.getData("SELECT * FROM PHIM WHERE LuotMua >= 10");

            if (Logout == "true")
            {
                Session["isLogined"] = null;
                Session["Username"] = null;
                return View();
            }

            return View();
        }

        public ActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(string Username, string Password)
        {
            
            if (Username == "admin" && Password == "adminpad")
            {
                Session["isLoginedQL"] = "true";
                if (Session["isLoginedQL"] == "true")
                {
                    return RedirectToAction("ReportHD", "Admin", new { area = "AdminArea" });
                }
            }
            else
            {
                Session["isLoginedQL"] = "false";
                //Không cần kiểm tra Username và Password là null do thẻ input đã có thuộc tính required
                if (db.getData($"SELECT * FROM KHACHHANG WHERE TenTKKH = '{Username}' AND MatKhauKH = '{Password}'").Count >= 1)
                {
                    Session["isLogined"] = "true";
                    Session["Username"] = Username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Error Login!";
                }
            }
            return View();
        }

        public ActionResult LoginPageNV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPageNV(string Username, string Password)
        {
            //Không cần kiểm tra Username và Password là null do thẻ input đã có thuộc tính required
            if (Username == "admin@gmail.com" && Password == "adminpad")
            {
                Session["isLoginedQL"] = "true";
                if (Session["isLoginedQL"] == "true")
                {
                    return RedirectToAction("ReportHD", "Admin", new { area = "AdminArea" });
                }
            }
            else
            {
                if (db.getData($"SELECT * FROM NHANVIEN WHERE Email = '{Username}' AND MatKhauNV = '{Password}'").Count >= 1)
                {
                    //Session["isLogined"] = "true";
                    //if (Session["isLogined"] == "true")
                    //{
                    //    Session["Username"] = Username;
                    //    return RedirectToAction("Index", "Home");
                    //}
                    Session["isLoginedQL"] = "false";
                    return RedirectToAction("Film", "Admin", new { area = "AdminArea" });
                }
                else
                {
                    ViewBag.ThongBao = "Error Login!";
                }
            }
            return View();
        }

        public ActionResult RegisterPage()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterPage(string Username, string Password, string Email)
        {
            if (Username != null && Password != null && Email != null)
            {
                if (db.checkDataUsername(Username))
                {
                    if (db.saveDataUser(Username, Password, Email))
                    {
                        Session["isLogined"] = "true";
                        if (Session["isLogined"]=="true")
                        {
                            Session["Username"] = Username;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            
            return View();
        }
        public ActionResult FilmDetail(int? IDPhim)
        {
            if (IDPhim != null)
            {
                Console.OutputEncoding = Encoding.GetEncoding("UTF-8");

                ViewBag.getFilmById123 = db.getData($"SELECT * FROM PHIM p, GIOIHANTUOI ght WHERE p.MaPhim={IDPhim} AND p.MaGHT=ght.MaGHT");
                ViewBag.getBL = db.getData($"SELECT bl.GhiChu, kh.TenTKKH, bl.NgayTao, bl.MaBL FROM BINHLUAN bl, PHIM p, KHACHHANG kh WHERE bl.MaPhim=p.MaPhim AND bl.MaKH=kh.MaKH AND p.MaPhim={IDPhim} ORDER BY bl.NgayTao DESC");

                //Get Linked Youtube Embed
                string input = ViewBag.getFilmById123[0][8];
                string delimiter = "https://www.youtube.com/watch?v=";
                string[] tokens = input.Split(new string[] { delimiter }, StringSplitOptions.None);
                if (tokens.Count() > 0)
                {
                    ViewBag.GetEmbedLink = "https://www.youtube.com/embed/" + tokens[1].Trim();
                }
                return View();
            }
            return View();
        }
        public ActionResult DeleteComment(string mabl, string maphim)
        {
            db.getData($"DELETE FROM BINHLUAN where MaBL = {mabl}");
            return RedirectToAction("FilmDetail", "Home", new { IDPhim = maphim });
        }
        [HttpPost]
        public ActionResult FilmDetail(int? IDPhim, string GhiChu, int? IDBL, string status)
        {
            if (status == "Post")
            {
                if (IDPhim != null && GhiChu != null)
                {
                    Console.OutputEncoding = Encoding.GetEncoding("UTF-8");

                    string tentk = Session["Username"].ToString();
                    int makh = db.getMaKH(tentk);
                    if (makh != 0)
                    {
                        bool isAdd = db.insertComment(IDPhim, makh, GhiChu);
                        if (isAdd)
                        {
                            return RedirectToAction("FilmDetail", "Home", new { IDPhim = IDPhim });
                        }
                        else
                        {
                            ViewBag.ThongBao = "Error Add!";
                        }
                    }
                    else
                    {
                        ViewBag.ThongBao = "Error MaKH = 0";
                    }

                }
            } else if (status == "Delete")
            {
                bool isDelete = db.deleteComment(IDBL);
                if (isDelete)
                {
                    return RedirectToAction("FilmDetail", "Home", new { IDPhim = IDPhim });
                }
            }
            return View();
        }

        public ActionResult AccountPage()
        {
            SQLData data = new SQLData();
            string tentk = Session["Username"].ToString();

            ViewBag.GetDate = data.getData($"SELECT CONVERT(date, vp.NgayDat) FROM VEPHIM vp, KHACHHANG kh WHERE kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' GROUP BY CONVERT(date, vp.NgayDat) ORDER BY CONVERT(date, vp.NgayDat) DESC");
            ViewBag.GetKH = data.getData($"SELECT kh.TenTKKH, kh.EmailKH, kh.MatKhauKH, lkh.TenLKH, lkh.ChietKhau FROM KHACHHANG kh, LOAIKH lkh WHERE kh.MaLoaiKH=lkh.MaLoaiKH AND kh.TenTKKH='{tentk}'");

            ViewBag.GetVP = data.getData($"SELECT vp.MaVe, p.TenPhim, pc.TenPC, STRING_AGG(vg.TenGheVG, ''), CONVERT(date, vp.NgayDat) " +
                                        $"FROM VEPHIM vp, KHACHHANG kh, LICHCHIEU lc, PHIM p, PHONGCHIEU pc, VE_GHE vg " +
                                        $"WHERE kh.TenTKKH='{tentk}' " +
                                        $"AND vp.MaKH=kh.MaKH " +
                                        $"AND vp.MaLC=lc.MaLC " +
                                        $"AND lc.MaPhim=p.MaPhim " +
                                        $"AND pc.MaPC=lc.MaPC " +
                                        $"AND vp.MaVe=vg.MaVe " +
                                        $"GROUP BY vp.MaVe, p.TenPhim, pc.TenPC, vp.NgayDat " +
                                        $"ORDER BY vp.NgayDat DESC");

            return View();
        }
        [HttpPost]
        public ActionResult AccountPage(string TenTK, string Email, string Pass)
        {
            SQLData data = new SQLData();
            
            if (Email != null && Pass != null)
            {
                bool isUpdate = data.updateKH(TenTK, Email, Pass);
                if (isUpdate)
                {
                    return RedirectToAction("AccountPage", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Update Fail!";
                }
            }
            else
            {
                ViewBag.ThongBao = "Null Email or Pass!";
            }

            return View();
        }

        public ActionResult Error_Page()
        {
            return View();
        }

        public ActionResult PointRule()
        {
            return View();
        }
        
        public ActionResult SearchResult(string searchValue = "")
        {
            if (searchValue == "")
            {
                List<Phim> GetPhimList = null;
                return PartialView(GetPhimList);
            } else
            {
                //string normalizedSearch = searchValue.Unidecode().ToLower();
                List<Phim> GetPhimList = db.GetPhimList($"SELECT * FROM PHIM WHERE TenPhim like N'%{searchValue}%'");
                return PartialView(GetPhimList);
            }
        }
    }
}