using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models
{
    public class YoutubeLink
    {
        public string GetEmbedLink(string youtubeLink)
        {
            // Kiểm tra xem link có chứa mã nhúng không
            if (youtubeLink.Contains("embed"))
            {
                return youtubeLink;
            }
            else
            {
                var videoId = youtubeLink.Split('=')[1];

                // Tạo mã nhúng từ ID video
                return $"https://www.youtube.com/embed/{videoId}";
            }
        }
    }
}