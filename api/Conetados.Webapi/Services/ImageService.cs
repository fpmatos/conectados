using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Services
{
    public class ImageService
    {
        public static Upload ResizeAndCompress(Stream imageStream, float maxHeight = 900f, float maxWidth = 900f, string mediaType = "image/jpg")
        {
            Upload newImage;

            using (var image = Image.FromStream(imageStream))
            using(MemoryStream ms = new MemoryStream())
            {
                int newWidth;
                int newHeight;
                Bitmap originalBMP = new Bitmap(imageStream);
                int originalWidth = originalBMP.Width;
                int originalHeight = originalBMP.Height;

                if (originalWidth > maxWidth || originalHeight > maxHeight)
                {
                    // To preserve the aspect ratio  
                    float ratioX = (float)maxWidth / (float)originalWidth;
                    float ratioY = (float)maxHeight / (float)originalHeight;
                    float ratio = Math.Min(ratioX, ratioY);
                    newWidth = (int)(originalWidth * ratio);
                    newHeight = (int)(originalHeight * ratio);
                }
                else
                {
                    newWidth = (int)originalWidth;
                    newHeight = (int)originalHeight;
                }                

                Bitmap bitMAP1 = new Bitmap(originalBMP, newWidth, newHeight);
                Graphics imgGraph = Graphics.FromImage(bitMAP1);

                if (mediaType == "image/png" || mediaType == "image/gif")
                {
                    imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                    imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                    if (mediaType == ".png")
                        bitMAP1.Save(ms, ImageFormat.Png);

                    if (mediaType == ".gif")
                        bitMAP1.Save(ms, ImageFormat.Gif);

                    bitMAP1.Dispose();
                    imgGraph.Dispose();
                    originalBMP.Dispose();
                }
                else if (mediaType == "image/jpg")
                {

                    imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                    imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                    myEncoderParameters.Param[0] = myEncoderParameter;

                    bitMAP1.Save(ms, jpgEncoder, myEncoderParameters);

                    bitMAP1.Dispose();
                    imgGraph.Dispose();
                    originalBMP.Dispose();

                }

                var blob = ms.ToArray();

                newImage = new Upload()
                {
                    Blob = blob,
                    MediaType = mediaType,
                    Width = newWidth,
                    Height = newHeight
                };                
            }
            
            return newImage;
        }


        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}