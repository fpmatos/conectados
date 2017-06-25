using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Infraestrutura
{
    public static class InjectorManager
    {
        private static Container simpleInjectorContainer { get; set; }
        public static void SetContainer(Container container)
        {
            simpleInjectorContainer = container;
        }

        public static T GetInstance<T>()
            where T: class
        {
            return simpleInjectorContainer.GetInstance<T>();
        }
    }
}