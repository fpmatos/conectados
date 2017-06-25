using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class Upload
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public byte[] Blob { get; set; }
        public string MediaType { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        //Remover
        public ICollection<Conteudo> Imagens { get; set; } = new List<Conteudo>();
        public ICollection<ImagemGaleria> ImagensGaleria { get; set; } = new List<ImagemGaleria>();
    }
}