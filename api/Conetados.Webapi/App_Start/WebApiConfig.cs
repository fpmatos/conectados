using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Conetados.Webapi.Models;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Conetados.Webapi.Filters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Swagger;

namespace Conetados.Webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // clear the supported mediatypes of the xml formatter
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //CamelCase json e ignore ReferenceLoopHandling
            var json = config.Formatters.JsonFormatter.SerializerSettings;            
            json.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            json.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            config.Filters.Add(new CustomFilterErrorAttribute());

            SwaggerConfig.Register();
        }
    }
}
