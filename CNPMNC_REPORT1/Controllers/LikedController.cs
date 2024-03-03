using CNPMNC_REPORT1.Factory;
using CNPMNC_REPORT1.Factory.FactoryPhim;
using CNPMNC_REPORT1.Factory.FactoryYT;
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

        // GET: Liked
        public ActionResult Index()
        {
            YeuThichFactory ytFactory = new CreateListLikedUser();
            ViewBag.YeuThich = ytFactory.CreateYT();

            subject.Attach(ytObserver);

            return View();
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