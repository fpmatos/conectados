using Conetados.Webapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Conetados.Webapi.Models;

namespace Conetados.Webapi.Infraestrutura.AutenticacaoFake
{
    public class AutenticacaoAdFake : IAutenticacaoAdService
    {
        public Usuario Autenticar(string nomeDeUsuario, string senha)
        {
            var roles = new List<string>();

            if (nomeDeUsuario.Equals("user1") && !string.IsNullOrWhiteSpace(senha))
                return new Usuario { Nome = "Fernando Matos", NomeDeUsuario = "user1", PerfilAdministrador = true, PerfilEditor = true };
            else
                throw new BusinessServiceException("Não foi possível efetuar o login. Dados de credencial inválidos.");
        }
    }
}