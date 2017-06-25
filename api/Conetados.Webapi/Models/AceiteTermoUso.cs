using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class AceiteTermoUso
    {
        public int Id { get; set; }
        public string MatriculaUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataAceite { get; set; }
    }
}