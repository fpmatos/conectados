using Conetados.Webapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Conetados.Webapi.Models;
using Conetados.Webapi.Infraestrutura.Autenticacao.AutenticacaoSap;

namespace Conetados.Webapi.Infraestrutura.Autenticacao.AutenticacaoAdDuratex
{
    public class AutenticacaoService : IAutenticacaoService
    {
        public Usuario Autenticar(AutenticacaoTipo autenticacaoTipo, string nomeDeUsuario, string Senha)
        {
            IAutenticacaoAdService adService;
            IAutenticacaoSapService sapService;
            Usuario usuario = null;

            switch (autenticacaoTipo)
            {
                case AutenticacaoTipo.Cms:
                    adService = InjectorManager.GetInstance<IAutenticacaoAdService>();
                    usuario = adService.Autenticar(nomeDeUsuario, Senha);
                    break;
                case AutenticacaoTipo.Mobile:
                    sapService = InjectorManager.GetInstance<IAutenticacaoSapService>();
                    usuario = sapService.Autenticar(nomeDeUsuario, Senha);
                    break;
                default:
                    break;
            }

            return usuario;
        }
    }
}