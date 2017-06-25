using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Services
{
    public class UsuarioContexto
    {
        public string NomeDeUsuario { get; set; }
        public string NomeDePerfil { get; set; }
        public bool AceitouTermoUso { get; set; }
        public string[] Roles { get; set; }
        public bool UsuarioCms { get { return Roles.Contains("UsuarioCms"); } }
        public bool UsuarioApp { get { return Roles.Contains("UsuarioApp"); } }
    }
}