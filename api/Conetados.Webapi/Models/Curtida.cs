using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class Curtida : IPossuiUsuarioApp
    {
        [Key, Column(Order =1)]
        public int ArtigoId { get; set; }
        [Key, Column(Order = 2)]
        public string UsuarioAppId { get; set; }
        public string UsuarioAppNome { get; set; }

        [ForeignKey("ArtigoId")]
        public Artigo Artigo { get; set; }
    }
}