using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class Comentario : IPossuiUsuarioApp, IAuditoria
    {
        public int Id { get; set; }
        public int ArtigoId { get; set; }        
        public string Mensagem { get; set; }
        public bool MarcadoComoImproprio { get; set; }
        public string UsuarioAppId { get; set; }
        public string UsuarioCmdId { get; set; }
        public string UsuarioCmsNome { get; set; }
        public string UsuarioAppNome { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
        public ICollection<Denuncia> Denuncias { get; set; } = new List<Denuncia>();

        [ForeignKey("ArtigoId")]
        public Artigo Artigo { get; set; }        
    }

    public class ComentarioDto
    {
        public int Id { get; set; }
        public int ArtigoId { get; set; }
        public string Mensagem { get; set; }
        public bool MarcadoComoImproprio { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataCriacao { get; set; } 
        public bool Denunciado { get; set; }
        public string ArtigoTitulo { get; set; }
        public bool PostadoPorModerador { get; set; }

    }

    public class ComentarioPostDto
    {
        public int ArtigoId { get; set; }
        public string Mensagem { get; set; }
    }
}