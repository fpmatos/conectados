using Conetados.Webapi;
using Conetados.Webapi.Infraestrutura;
using Conetados.Webapi.Models;
using Conetados.Webapi.Services;
using Conetados.Webapi.Services.AceitesTermosUso;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Conectados.Webapi.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var appNameHeaderItem = context.Request.Headers.FirstOrDefault(item => item.Key.Equals("App"));
            IAutenticacaoService Autenticacaoservice;
            Usuario user = null;
            AutenticacaoTipo TipoAutenticacao = 0;
            try
            {
                if (appNameHeaderItem.Value != null && appNameHeaderItem.Value.Length > 0)
                {
                    TipoAutenticacao = RetornarTipoAutenticacao(appNameHeaderItem.Value[0]);
                    Autenticacaoservice = InjectorManager.GetInstance<IAutenticacaoService>();
                    user = Autenticacaoservice.Autenticar(TipoAutenticacao, context.UserName, context.Password);
                }

            }
            catch (BusinessServiceException exception)
            {
                context.SetError("invalid_grant", exception.Message);
                return;
            }
            catch
            {
                context.SetError("invalid_grant", "Um erro crítico ocorreu durante a autenticação. Contacte a área responsável");
                return;
            }
                       

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var aceitouTermoDeUso = VerificarSeUsuarioJaAceitouTermoDeuso(user.NomeDeUsuario);

            identity.AddClaim(new Claim("NomeDeUsuario", user.NomeDeUsuario));
            identity.AddClaim(new Claim("NomeDePerfil", user.Nome));
            identity.AddClaim(new Claim("AceitouTermoDeUso", aceitouTermoDeUso.ToString()));

            if (user.PerfilAdministrador)
                identity.AddClaim(new Claim(ClaimTypes.Role, "Administrador"));

            if (user.PerfilEditor)
                identity.AddClaim(new Claim(ClaimTypes.Role, "Editor"));

            if (user.PerfilMobile)
                identity.AddClaim(new Claim(ClaimTypes.Role, "Mobile"));

            if(TipoAutenticacao == AutenticacaoTipo.Cms)
                identity.AddClaim(new Claim(ClaimTypes.Role, "UsuarioCms"));
            
            if (TipoAutenticacao == AutenticacaoTipo.Mobile)
                identity.AddClaim(new Claim(ClaimTypes.Role, "UsuarioApp"));

            context.Validated(identity);
        }

        private AutenticacaoTipo RetornarTipoAutenticacao(string appNome)
        {
            var result = AutenticacaoTipo.Mobile;
            
            switch(appNome)
            {
                case "cms":
                    result = AutenticacaoTipo.Cms;
                    break;
                case "mobile":
                    result = AutenticacaoTipo.Mobile;
                    break;
                default:
                    throw new BusinessServiceException("Tipo de aplicação não informado para autenticação.");

            }

            return result;
        }

        private bool VerificarSeUsuarioJaAceitouTermoDeuso(string usuarioId)
        {
            try
            {
                var db = new Contexto();
                var aceitouTermoDeUso = db.TermoUsoAceites.Any(item => item.MatriculaUsuario.Equals(usuarioId));

                return aceitouTermoDeUso;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}