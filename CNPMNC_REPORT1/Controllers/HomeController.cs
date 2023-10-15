using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CNPMNC_REPORT1.Models;
using System.Net;
using System.Net.Mail;
namespace CNPMNC_REPORT1.Controllers
{
    public class HomeController : Controller
    {

        SQLData db = new SQLData();
        public ActionResult Index()
        {
            //Lấy danh sách phim đang khởi chiếu
            ViewBag.NowShowing = db.getData("SELECT * FROM PHIM WHERE NgayCongChieu <= GETDATE()");
            //Lấy danh sách phim sắp khởi chiếu
            ViewBag.ComingSoon = db.getData("SELECT * FROM PHIM WHERE NgayCongChieu > GETDATE()");
            //Lấy danh sách phim xem nhiều nhất
            ViewBag.MostWatching = db.getData("SELECT * FROM PHIM WHERE LuotMua>=10");
            return View();
        }

        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult RegisterPage()
        {
            return View();
        }

        public ActionResult FilmDetail(int? IDPhim)
        {
            if (IDPhim != null)
            {
                Console.OutputEncoding = Encoding.GetEncoding("UTF-8");

                ViewBag.getFilmById123 = db.getData($"SELECT * FROM PHIM p, GIOIHANTUOI ght WHERE p.MaPhim={IDPhim} AND p.MaGHT=ght.MaGHT");

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

        

    }
}