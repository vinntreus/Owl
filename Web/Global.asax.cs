using System.Web.Mvc;
using System.Web.Routing;
using Core;
using Munq.MVC3;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
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

            //            // TODO: Register Dependencies in Global.asax Application_Start
            //            // var ioc = MunqDependencyResolver.Container;
            //            // Munq.Configuration.ConfigurationLoader.FindAndRegisterDependencies(ioc); // Optional
            var ioc = MunqDependencyResolver.Container;
            ioc.Register(r => Store.DocumentStore);
            ioc.Register<IHandleUsers, Users>();
        }
    }
}