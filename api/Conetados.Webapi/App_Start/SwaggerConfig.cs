using System.Web.Http;
using WebActivatorEx;
using Conetados.Webapi;
using Swashbuckle.Application;
using System;
using System.IO;

namespace Swagger
{
    public class SwaggerConfig
    {
        public static void Register()
        {

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var commentsFileName = "references" + ".XML";
            var commentsFile = Path.Combine(baseDirectory, commentsFileName);

            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Conectados Referência");
                    c.IncludeXmlComments(commentsFile);
                })
                .EnableSwaggerUi(c =>
                {

                });
        }

    }
}