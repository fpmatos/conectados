using Conetados.Webapi.Infraestrutura;
using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Services
{
    public class UploadArquivoService
    {
        public UploadModel SalvarArquivo(Upload upload)
        {
            var contexto = InjectorManager.GetInstance<Contexto>();

            contexto.Uploads.Add(upload);

            contexto.SaveChanges();

            return new UploadModel
            {
                ArquivoId = upload.Id,
                Nome = upload.NomeArquivo,
                MediaType = upload.MediaType,
                Width = upload.Width.Value,
                Height = upload.Height.Value
            };
        } 

        public void ExcluirArquivo(int Id)
        {
            var contexto = InjectorManager.GetInstance<Contexto>();

            //não excluir imagens que pertençam a um layout
            var arquivo = contexto.Uploads.FirstOrDefault(item => item.Id.Equals(Id)
                && (!item.Imagens.Any(x => x.Artigo is Layout)
                || !item.ImagensGaleria.Any(x => x.Galeria.Artigo is Layout)));

            if (arquivo == null)
                return;

            contexto.Uploads.Remove(arquivo);
            contexto.SaveChanges();

        }
    }
}