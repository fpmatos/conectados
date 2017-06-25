using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeDeUsuario { get; set; }
        public bool PerfilAdministrador { get; set; }
        public bool PerfilEditor { get; set; }
        public bool PerfilMobile { get; set; }
    }
}