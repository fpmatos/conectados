using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class AlternativaResposta : IPossuiUsuarioApp, IAuditoria
    {
        [Key, Column(Order = 1)]
        public int AlternativaId { get; set; }
        [Key, Column(Order = 2)]
        public string UsuarioAppId { get; set; }
        public string UsuarioAppNome { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }

        [ForeignKey("AlternativaId")]
        public Alternativa Alternativa { get; set; }        
    }
}