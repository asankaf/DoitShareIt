﻿using System.Web.Routing;
using SimpleCompression;
using SimpleCompression.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.SimpleCompression), "Start")]
namespace $rootnamespace$.App_Start
{
    public class SimpleCompression
    {
        public static void Start()
        {
            var defaultConfig = new SimpleCompressionConfiguration()
            {
                Compress = true,
                Compressor = new YUICompression(),
                ClientVersionPrefix = "ver1",
                Disable = false,
                FolderForCachedResources = "SuperCache",
            };
            SimpleCompressionConfiguration.SetDefaultConfiguration(defaultConfig);

            var routes = RouteTable.Routes;
            AddRoutes(routes, defaultConfig);
        }

        private static void AddRoutes(RouteCollection routes, SimpleCompressionConfiguration configuration)
        {
            routes.Add(new Route(configuration.FolderForCachedResources + "{file}", new SimpleCompressionRouteHandler()));
        }
    }
}
