using CNPMNC_REPORT1.Factory;
using CNPMNC_REPORT1.Factory.FactoryPhim;
using CNPMNC_REPORT1.Factory.FactoryYT;
using CNPMNC_REPORT1.Iterator.YTIterator;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.Observer;
using CNPMNC_REPORT1.Observer.Object;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPMNC_REPORT1.Controllers
{
    public class LikedController : Controller
    {

        private readonly static SubjectObserver subject = new SubjectObserver();
        private static YeuThichObserver ytObserver = new YeuThichObserver();
        private static YeuThichFactory ytFactory = new CreateListLikedUser();

        // GET: Liked
        public ActionResult Index()
        {
            ViewBag.YeuThich = ytFactory.CreateYT();

            subject.Attach(ytObserver);

            return View();
        }

        public ActionResult SortDSYT(string value)
        {
            List<YeuThich> ytList = ytFactory.CreateYT();

            // Sử dụng YTCollect để lưu lại danh sách yt
            YTCollect ytCollect = new YTCollect(ytList);

            // Sử dụng cách duyệt dành cho yt
            YTIterator iterator = (YTIterator)ytCollect.CreateIterator();

            List<YeuThich> sortedList;
            switch (value)
            {
                case "1":
                    sortedList = iterator.SortMostLiked();
                    break;
                case "2":
                    sortedList = iterator.SortLeastLiked();
                    break;
                default:
                    sortedList = iterator.SortMostLiked();
                    break;
            }

            return PartialView("YTList", sortedList);
        }

        public ActionResult AddFilmLiked(string MaPhim)
        {
            YeuThich yt = new YeuThich();

            subject.DetachAll();
            subject.Attach(ytObserver);

            SQLUser user = SQLUser.Instance;
            bool success = false;

            if (user.KH != null)
            {
                yt.MaPhim = MaPhim;
                yt.MaKH = user.KH.MaKH;

                success = true;
                subject.Notify(yt, ActionType.Add);
            }

            return Json(new { success = success });
        }

        public ActionResult RemoveFilmLiked(string MaPhim)
        {
            YeuThich yt = new YeuThich();

            SQLUser user = SQLUser.Instance;

            yt.MaPhim = MaPhim;
            yt.MaKH = user.KH.MaKH;

            subject.Notify(yt, ActionType.Remove);

            return RedirectToAction("Index", "Liked");
        }
    }
}