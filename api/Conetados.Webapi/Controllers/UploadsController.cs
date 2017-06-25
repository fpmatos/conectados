using Conetados.Webapi.Controllers.Base;
using Conetados.Webapi.Infraestrutura;
using Conetados.Webapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Conetados.Webapi.Controllers
{
    public class UploadsController: ControllerBase
    {
        [Route("Api/Uploads/SalvarArquivo")]
        public async Task<UploadModel> Post()
        {
            var uploadService = InjectorManager.GetInstance<UploadArquivoService>();
            UploadModel model = null;

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider).
                 ContinueWith(o =>  
                 {
                     var fileContent = provider.Contents.SingleOrDefault();

                     if (fileContent != null)
                     {
                         var fileName = fileContent.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                         var type = fileContent.Headers.ContentType.MediaType;
                         var blob = fileContent.ReadAsByteArrayAsync().Result;

                         var stream = fileContent.ReadAsStreamAsync().Result;
                         var upload = ImageService.ResizeAndCompress(stream);

                         model = uploadService.SalvarArquivo(upload);
                     }
                 });

            return model;
        }

        public void Delete(int id)
        {
            var uploadService = InjectorManager.GetInstance<UploadArquivoService>();
            uploadService.ExcluirArquivo(id);
        }
    }
}