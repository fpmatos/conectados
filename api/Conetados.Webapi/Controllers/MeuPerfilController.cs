using Conetados.Webapi.Controllers.Base;
using Conetados.Webapi.Infraestrutura;
using Conetados.Webapi.Models;
using Conetados.Webapi.Services;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Conetados.Webapi.Controllers
{
    [Authorize]
    public class MeuPerfilController: ControllerBase
    {
        [Route("Api/MeuPerfil/")]
        public UsuarioContexto Get()
        {
            return UsuarioContexto;
        }

    }
}