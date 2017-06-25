using Conetados.Webapi.Infraestrutura;
using Conetados.Webapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Conetados.Webapi.Controllers.Base
{
    public class ControllerSemODataBase: ApiController
    {
        public UsuarioContexto UsuarioContexto { get; private set; }

        public ControllerSemODataBase()
        {
            UsuarioContexto = InjectorManager.GetInstance<UsuarioContexto>();
            UsuarioContexto.NomeDeUsuario = RetornarCampoIdentityClaim("NomeDeUsuario");
            UsuarioContexto.NomeDePerfil = RetornarCampoIdentityClaim("NomeDePerfil");
            UsuarioContexto.Roles = RetornarRoles();
        }

        private string RetornarCampoIdentityClaim(string nomeDoCampo)
        {
            string result = null;
            var claim = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(item => item.Type.Equals(nomeDoCampo));

            if (claim != null)
                result = claim.Value;

            return result;
        }

        public string[] RetornarRoles()
        {
            return ((ClaimsIdentity)User.Identity)
                .Claims.Where(claim => claim.Type == ClaimTypes.Role)
                .Select(item => item.Value)
                .ToArray();
        }
    }
}