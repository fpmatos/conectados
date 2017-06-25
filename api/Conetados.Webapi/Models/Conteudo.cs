using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    [Table("Conteudos")]
    public class Conteudo : IConteudo,
        IParagrafo,
        ITitulo,
        IImagem,
        IGaleria,
        IEnquete,
        IVideo
    {
        public int Id { get; set; }
        public int Ordem { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TipoConteudo TipoConteudo { get; set; }
        //Remover
        public int ArtigoId { get; set; }
        [ForeignKey("ArtigoId")]
        public ArtigoBase Artigo { get; set; }
        //Paragrafo
        public string TextoParagrafo { get; set; }
        //Titulo
        public int? Importancia { get; set; }
        public string TextoTitulo { get; set; }
        //Imagem
        public string Descricao { get; set; }
        public int? UploadId { get; set; }
        [ForeignKey("UploadId")]
        public Upload Upload { get; set; }
        //Galeria
        public ICollection<ImagemGaleria> ImagensGaleria { get; set; } = new List<ImagemGaleria>();
        //Enquete
        public ICollection<Alternativa> Alternativas { get; set; } = new List<Alternativa>();
        public DateTime? DataEncerramentoEnquete { get; set; }
        [NotMapped]
        public int? RespostaEnqueteId { set; get; }
        //Vídeo
        public string YoutubeVideoId { get; set; }        
    }
}