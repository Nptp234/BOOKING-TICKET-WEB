using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPMNC_REPORT1.Prototype.BinhLuanPrototype
{
    public class CommentConcrete : ICommentPrototype
    {

        public override ICommentPrototype Clone()
        {
            return (ICommentPrototype)this.MemberwiseClone();
        }

        public override string Render()
        {
            string html = $@"
            <div class='comment mt-2 text-justify float-left'>
                <img src='https://i.imgur.com/yTFUilP.jpg' alt='' class='rounded-circle' width='30' height='30'>
                <h4 style='margin-right: 10px; margin-bottom: 20px;'>{TenTK}</h4>
                <span>{DateTime.Parse(NgayTao).ToString("MMM dd, yyyy HH:mm:ss tt")}</span>
                <br>
                <p style='margin-left: 20px; margin-top: 20px;'>{GhiChu}</p>";

            if (HttpContext.Current.Session["Username"] as string == TenTK)
            {
                html += $@"
                <a href='{new UrlHelper(HttpContext.Current.Request.RequestContext).Action("DeleteComment", "Home", new { maPhim = MaPhim, maBL = MaBL })}' name='status' value='Delete' style='color: white; float: right; padding-bottom: 20px;'>Xoá bình luận</a>";
            }

            html += "</div>";

            return html;
        }
    }
}

