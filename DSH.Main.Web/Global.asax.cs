using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DSH.Main.Web.RESTComponents;
using DSH.Main.Web.RESTComponents.ModelBinder;

namespace DSH.Main.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "Home", action = "Index"} // Parameter defaults
            );

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}", // URL with parameters
            //    new { controller = "REST", action = "Index"} // Parameter defaults
            //);

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            var c = new RestfulMVC();
            c.Init();
            ModelBinders.Binders.DefaultBinder = new RestfulDefaultModelBinder();
            RegisterRoutes(RouteTable.Routes);
        }
    }
}