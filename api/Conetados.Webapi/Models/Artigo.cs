using Conetados.Webapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    [Table("Artigos")]
    public abstract class ArtigoBase : IPossuiUsuarioCms, IAuditoria, ISoftDelete
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public byte[] Thumbnail { get; set; }
        public string UsuarioCmsId { get; set; }
        public string UsuarioCmsNome { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Conteudo> Conteudos { get; set; } = new List<Conteudo>();        
    }

    public class Artigo : ArtigoBase
    {
        public DateTime? DataPublicacao { get; set; }
        public int LayoutId { get; set; }

        [ForeignKey("LayoutId")]
        public Layout Layout { get; set; }
        public ICollection<Curtida> Curtidas { get; set; } = new List<Curtida>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        [NotMapped]
        public int? TotalCurtidas { get; set; }
        [NotMapped]
        public int? TotalComentarios { get; set; }
        [NotMapped]        
        public bool? UsuarioJaCurtiu { get; set; }
    }

    public class Layout : ArtigoBase    
    {

    }

    public class ArtigoSumaryDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public byte[] Thumbnail { get; set; }
        public int LayoutId { get; set; }
        public string LayoutNome { get; set; }
        public string UsuarioCmsId { get; set; }
        public string UsuarioCmsNome { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public bool Ativo { get; set; }
        public int TotalCurtidas { get; set; }
        public int TotalComentarios { get; set; }
        public int TotalComentariosImproprios { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }

    public class ArtigoBasicoFeedDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public byte[] Thumbnail { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public int TotalCurtidas { get; set; }
        public int TotalComentarios { get; set; }
        public bool UsuarioJaCurtiu { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }

    public class FeedArtigoPesquisaDTO
    {
        public bool TodosRegistrosLidos { get; set; }
        public int? PaginaCorrente { get; set; }
        public int? PorRequisicao { get; set; }
        public int? TagId { get; set; }
        public bool ApenasCurtidos { get; set; }
        public string TituloPesquisa { get; set; }
    }

    public class FeedArtigoResultadotDto
    {
        public FeedArtigoPesquisaDTO Metadata {  get; set; }
        public IEnumerable<ArtigoBasicoFeedDTO> Data { get; set; }
    }


}