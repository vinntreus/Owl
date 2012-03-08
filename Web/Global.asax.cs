﻿using System.Web.Mvc;
using System.Web.Routing;
using Core;
using Munq.MVC3;
using Raven.Client;
using Web.Attributes;
using Web.Security;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
			filters.Add(new RequireAuthorizationAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new MunqDependencyResolver());
            var ioc = MunqDependencyResolver.Container;
            ioc.Register(r => Store.DocumentStore);
            ioc.Register<IDocumentSession>(r => r.Resolve<IDocumentStore>().OpenSession());
            ioc.Register<IStore, Core.Store>();
            ioc.Register<ICommandExecutor, CommandExecutor>();
			ioc.Register<IAuthenticator, FormsAuthenticator>();
        }
    }
}