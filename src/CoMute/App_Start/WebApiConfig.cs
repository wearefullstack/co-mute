using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CoMute.Lib.services;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace CoMute.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //var container = new UnityContainer();
            //container.RegisterType<IUserService, UserService>(/*new HierarchicalLifetimeManager()*/);
            //config.DependencyResolver = new UnityResolver(container);


            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                                       name: "Default API 1",
                                       routeTemplate: "api/{controller}/{action}/{id}",
                                       defaults: new { id = RouteParameter.Optional }
                                      );

            config.Routes.MapHttpRoute(
                                       name: "Default API 2",
                                       routeTemplate: "api/{controller}/{id}",
                                       defaults: new { id = RouteParameter.Optional }
                                      );

        }
    }
}
