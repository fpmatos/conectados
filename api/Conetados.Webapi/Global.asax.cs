using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Conetados.Webapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(InjectorConfig.Register);

            if (ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"] != "false")
            {
                var configuration = new Migrations.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }
        }
    }
}
