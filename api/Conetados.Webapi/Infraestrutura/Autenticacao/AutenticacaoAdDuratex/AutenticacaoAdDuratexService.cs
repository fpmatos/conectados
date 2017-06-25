using Conetados.Webapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Conetados.Webapi.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;

namespace Conetados.Webapi.Infraestrutura
{
    public class AutenticacaoAdDuratexService : IAutenticacaoAdService
    {
        public Usuario Autenticar(string nomeDeUsuario, string senha)
        {
            var listaDeGrupos = RetornarGruposAdministradores();

            listaDeGrupos.AddRange(RetornarGruposEditores());

            var credenciais = new
            {
                Login = nomeDeUsuario,
                Senha = senha,
                ListaGrupos = listaDeGrupos
            };

            var autenticacao = this.PostCredenciais(credenciais);

            if (autenticacao == null)
                throw new BusinessServiceException("Usuário ou senha Inválido!");

            if (autenticacao.Grupos == null || autenticacao.Grupos.Count == 0)
                throw new BusinessServiceException("Usuário não faz perte de nenhum perfil de acesso!");

            var usuario = new Usuario { NomeDeUsuario = nomeDeUsuario, Nome = autenticacao.Nome };

            usuario.PerfilAdministrador = autenticacao.Grupos.Exists(grupo => RetornarGruposAdministradores().Exists(_grupoAdmin => _grupoAdmin.Equals(grupo)));
            usuario.PerfilEditor = autenticacao.Grupos.Exists(grupo => RetornarGruposEditores().Exists(_grupoAdmin => _grupoAdmin.Equals(grupo)));

            return usuario;
        }

        private List<string> RetornarGruposAdministradores()
        {
            return new List<string>() { { "GUEC_CONECTADOS_ADM" }, { "GU_CDV-01" }, { "GU_CDV-02" } };
        }

        private List<string> RetornarGruposEditores()
        {
            return new List<string>() { { "GUEC_CONECTADOS_EDIT" } };
        }

        private AutenticacaoModel PostCredenciais(object credenciais)
        {
            var urlDuratexAdService = ConfigurationManager.AppSettings["SSOBaseURL"];
            var client = new RestClient(urlDuratexAdService);
            var request = new RestRequest("autenticar", Method.POST);
            request.AddParameter("text/json", request.JsonSerializer.Serialize(credenciais), ParameterType.RequestBody);

            var response = client.Execute(request);

            var autenticacao = JsonConvert.DeserializeObject<AutenticacaoModel>(response.Content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new Exception(autenticacao.Message);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception("Falha no serviço de auteticação, por favor tente mais tarde!");

            return autenticacao;
        }
    }
}