using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Factory;
using System.Net;
using System.Net.Mail;
using System.Collections;
using Unidecode.NET;
using CNPMNC_REPORT1.Factory.FactoryBL;
using CNPMNC_REPORT1.Factory.FactoryGHT;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.Models.User;

namespace CNPMNC_REPORT1.Controllers
{
    public class HomeController : Controller
    {
        PhimFactory factoryPhim;
        BinhLuanFactory factoryBL;
        GHTFactory factoryGHT;
        SQLData123 db = new SQLData123();

        public ActionResult Index(string Logout)
        {
            SingletonPhim singletonPhim = SingletonPhim.Instance;
            singletonPhim.ResetInstance();

            //Lấy danh sách phim đang khởi chiếu
            factoryPhim = new NowShowingFilmFactory();
            ViewBag.NowShowing = factoryPhim.CreatePhim();

            //Lấy danh sách phim sắp khởi chiếu
            factoryPhim = new ComingSoonFilmFactory();
            ViewBag.ComingSoon = factoryPhim.CreatePhim();

            //Lấy danh sách phim xem nhiều nhất
            factoryPhim = new MostWatchingFilmFactory();
            ViewBag.MostWatching = factoryPhim.CreatePhim();

            if (Logout == "true")
            {
                Session["isLogined"] = null;
                Session["Username"] = null;
                return View();
            }

            return View();
        }

        public ActionResult LoginPage(string Username, string Password, string status)
        {
            SQLUser sQLUser = SQLUser.Instance;
            UserAccount userAccount;

            if (Username == "admin" && Password == "adminpad")
            {
                Session["isLoginedQL"] = "true";
                return RedirectToAction("ReportHD", "Admin", new { area = "AdminArea" });
            }
            else
            {
                Session["isLoginedQL"] = "false";

                if (status == "Check")
                {
                    userAccount = new KhachHang();

                    bool check = sQLUser.KiemTraThongTinDangNhap(Username, Password, userAccount.UserType);

                    if (check)
                    {
                        Session["isLogined"] = "true";

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ThongBao = "Error Login!";
                    }
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

        public ActionResult FilmDetail(string MaPhim)
        {
            if (MaPhim != null)
            {
                Console.OutputEncoding = Encoding.GetEncoding("UTF-8");

                SingletonPhim singletonPhim = SingletonPhim.Instance;
                List<Phim> lsPhim = singletonPhim.CreatePhim();

                int maPhim = lsPhim.FindIndex(x => x.MaPhim == MaPhim);

                if (maPhim != -1)
                {
                    Phim phim = lsPhim[maPhim];

                    //lấy link youtube trailer
                    YoutubeLink youtube = new YoutubeLink();

                    string youtubeLink = phim.Trailer;
                    string embedLink = youtube.GetEmbedLink(youtubeLink);

                    ViewBag.EmbedLink = embedLink;

                    //lấy danh sách bình luận
                    factoryBL = new CreateBLWithFilm(MaPhim);
                    List<BinhLuan> dsBL = factoryBL.CreateBL();

                    ViewBag.BinhLuan = dsBL;

                    //xác định giới hạn tuổi
                    factoryGHT = new CreateAllGHT();
                    List<GioiHanTuoi> dsGHT = factoryGHT.CreateGHT();

                    int number;
                    if (int.TryParse(phim.MaGHT, out number)==false)
                    {
                        return View(phim);
                    }
                    else
                    {
                        int maGHT = int.Parse(phim.MaGHT);
                        foreach (GioiHanTuoi GHT in dsGHT)
                        {
                            if (int.Parse(GHT.MaGHT) == maGHT)
                            {
                                phim.TenGHTP = GHT.TenGHT;
                            }
                        }
                    }

                    return View(phim);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult FilmDetail(string IDPhim, string GhiChu, string IDBL, string status)
        {
            SQLBinhLuan sQLBinhLuan = new SQLBinhLuan();
            BinhLuan bl = new BinhLuan();
            if (status == "Post")
            {
                if (IDPhim != null && GhiChu != null)
                {
                    Console.OutputEncoding = Encoding.GetEncoding("UTF-8");

                    string tentk = Session["Username"].ToString();

                    if (tentk != null)
                    {
                        DateTime now = DateTime.Now;
                        bl.NgayTao = now.ToString();
                        bl.MaPhim = IDPhim;
                        bl.TenTK = tentk;
                        bl.GhiChu = GhiChu;
                        bl.TrangThai = "Active";

                        bool isAdd = sQLBinhLuan.ThemBL(bl);
                        if (isAdd)
                        {
                            return RedirectToAction("FilmDetail", "Home", new { MaPhim = IDPhim });
                        }
                        else
                            ViewBag.ThongBao = "Error Add!";
                    }
                    else
                        ViewBag.ThongBao = "Error TenTK = null!";

                }
            } 
            else if (status == "Delete")
            {
                bool isDelete = sQLBinhLuan.XoaBL(IDBL);
                if (isDelete)
                {
                    return RedirectToAction("FilmDetail", "Home", new { IDPhim = IDPhim });
                }
                else
                    ViewBag.ThongBao = "Error Delete!";
            }
            return View();
        }

        public ActionResult AccountPage()
        {
            SQLData123 data = new SQLData123();
            string tentk = Session["Username"].ToString();

            ViewBag.GetDate = data.getData($"SELECT vp.NgayDat FROM VEPHIM vp, KHACHHANG kh WHERE kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' GROUP BY vp.NgayDat ORDER BY CONVERT(date, vp.NgayDat) DESC");
            ViewBag.GetKH = data.getData($"SELECT kh.TenTKKH, kh.EmailKH, kh.MatKhauKH, lkh.TenLKH, lkh.ChietKhau FROM KHACHHANG kh, LOAIKH lkh WHERE kh.MaLoaiKH=lkh.MaLoaiKH AND kh.TenTKKH='{tentk}'");

            ViewBag.GetVP = data.getData($"SELECT vp.MaVe, p.TenPhim, pc.TenPC, STRING_AGG(vg.TenGheVG, ''), vp.NgayDat, lc.NgayLC, xc.GioXC " +
                                        $"FROM VEPHIM vp, KHACHHANG kh, LICHCHIEU lc, PHIM p, PHONGCHIEU pc, VE_GHE vg, XUATCHIEU xc " +
                                        $"WHERE kh.TenTKKH='{tentk}' " +
                                        $"AND vp.MaKH=kh.MaKH " +
                                        $"AND vp.MaLC=lc.MaLC " +
                                        $"AND lc.MaPhim=p.MaPhim " +
                                        $"AND pc.MaPC=lc.MaPC " +
                                        $"AND vp.MaVe=vg.MaVe " +
                                        $"AND lc.MaXC=xc.MaXC " +
                                        $"GROUP BY vp.MaVe, p.TenPhim, pc.TenPC, vp.NgayDat, lc.NgayLC, xc.GioXC " +
                                        $"ORDER BY vp.NgayDat DESC");

            return View();
        }
        [HttpPost]
        public ActionResult AccountPage(string TenTK, string Email, string Pass)
        {
            SQLData123 data = new SQLData123();
            
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