using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Conetados.Webapi.Models;

namespace Conetados.Webapi.Infraestrutura.Autenticacao.AutenticacaoSap
{
    public class AutenticacaoSapFakeService : IAutenticacaoSapService
    {
        public Usuario Autenticar(string matricula, string cpf)
        {
            if(string.IsNullOrEmpty(matricula) || !matricula.Equals("0123456") || string.IsNullOrEmpty(cpf))
                throw new BusinessServiceException("Não foi possível efetuar o login. Dados de credencial inválidos.");

            return new Usuario
            {
                Nome = "Fernando Matos",
                NomeDeUsuario = matricula,
                PerfilAdministrador = false,
                PerfilEditor = false,
                PerfilMobile = true
            };
        }
    }
}