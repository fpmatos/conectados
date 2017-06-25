using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Conetados.Webapi.Services
{
    public class YoutubeThumbnailService
    {
        public byte[] RetornarThumbnailVideo(string youtubeId)
        {
            WebClient wc = new WebClient();
            MemoryStream stream = new MemoryStream(wc.DownloadData(string.Format("http://img.youtube.com/vi/{0}/default.jpg", youtubeId)));
            BinaryReader reader = new BinaryReader(stream);
            byte[] photo = reader.ReadBytes((int)stream.Length);

            //create thumbnail
            var upload = ThumbnailService.CreateThumbnail(stream);

            return upload.Blob;
        }
    }
}