using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Services
{
    public static class ThumbnailService
    {
        public static Upload CreateThumbnail(Stream stream)
        {
            return ImageService.ResizeAndCompress(stream, 415, 260);
        }
    }
}