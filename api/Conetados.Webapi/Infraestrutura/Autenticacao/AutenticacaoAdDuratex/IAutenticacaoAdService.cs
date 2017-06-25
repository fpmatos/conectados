using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Infraestrutura
{
    public interface IAutenticacaoAdService
    {
        Usuario Autenticar(string nomeDeUsuario, string senha);
    }
}