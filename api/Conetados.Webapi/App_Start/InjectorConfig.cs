using Conetados.Webapi.Infraestrutura;
using Conetados.Webapi.Infraestrutura.Autenticacao.AutenticacaoAdDuratex;
using Conetados.Webapi.Infraestrutura.Autenticacao.AutenticacaoSap;
using Conetados.Webapi.Infraestrutura.AutenticacaoFake;
using Conetados.Webapi.Services;
using Conetados.Webapi.Services.AceitesTermosUso;
using Conetados.Webapi.Services.Artigos;
using Conetados.Webapi.Services.Comentarios;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Conetados.Webapi
{
    public static class InjectorConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new Container();

            BindDependencies(container);

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            InjectorManager.SetContainer(container);
        }

        private static void BindDependencies(Container container)
        {
            container.Register<Contexto, Contexto>(new WebApiRequestLifestyle());
            container.Register<UsuarioContexto, UsuarioContexto>(new WebApiRequestLifestyle());

 #if DEBUG
            container.Register<IAutenticacaoAdService, AutenticacaoAdFake>(Lifestyle.Singleton);
            container.Register<IAutenticacaoSapService, AutenticacaoSapFakeService>(Lifestyle.Singleton);
#endif

#if !DEBUG
            container.Register<IAutenticacaoAdService, AutenticacaoAdDuratexService>(Lifestyle.Singleton);
            container.Register<IAutenticacaoSapService, AutenticacaoSapFakeService>(Lifestyle.Singleton);
#endif

            container.Register<IAutenticacaoService, AutenticacaoService>(Lifestyle.Singleton);
            container.Register<UploadArquivoService, UploadArquivoService>(Lifestyle.Singleton);
            container.Register<AceitesTermosDeUsoService, AceitesTermosDeUsoService>(new WebApiRequestLifestyle());
        }
    }
}