using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conetados.Webapi.Models
{
    public class ImagemGaleria : IImagem
    {
        public int Id { get; set; }
        public int Ordem { get; set; }
        public string Descricao { get; set; }

        public Upload Upload { get; set; }

        public int? UploadId { get; set; }

        public int GaleriaId { get; set; }
        [ForeignKey("GaleriaId")]
        public Conteudo Galeria { get; set; }
    }
}