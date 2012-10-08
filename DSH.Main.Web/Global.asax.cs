﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using DSH.Main.Web.RESTComponents;
using DSH.Main.Web.RESTComponents.ModelBinder;
using DSH.Access.DataModels;


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
            routes.MapRoute(
                "GetUserInfo",
                "{controller}/{action}/{id}",
                new { controller = "User", action = "UserInfo", id = (string)null });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            var c = new RestfulMVC();
            c.Init();
            ModelBinders.Binders.DefaultBinder = new RestfulDefaultModelBinder();
            RegisterRoutes(RouteTable.Routes);

            try
            {
                AutoMapperConfiguration.Configure();
                Mapper.AssertConfigurationIsValid();
            }
            catch (Exception)
            {
                //Some mapping is not working
            }
        }
    }

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {

            Mapper.CreateMap<Users, DSH.DataAccess.User>();
            Mapper.CreateMap<DSH.DataAccess.User, Users>();

            Mapper.CreateMap<DSH.Access.DataModels.Post, DSH.DataAccess.Post>();
            Mapper.CreateMap<DSH.DataAccess.Post, DSH.Access.DataModels.Post>();

            Mapper.CreateMap<DSH.Access.DataModels.Comment, DSH.DataAccess.Post>();
            Mapper.CreateMap<DSH.DataAccess.Post, DSH.Access.DataModels.Comment>();

            Mapper.CreateMap<DSH.Access.DataModels.Vote, DSH.DataAccess.Vote>();
            Mapper.CreateMap<DSH.DataAccess.Vote, DSH.Access.DataModels.Vote>();

            Mapper.CreateMap<DSH.Access.DataModels.VoteType, DSH.DataAccess.VoteType>();
            Mapper.CreateMap<DSH.DataAccess.VoteType, DSH.Access.DataModels.VoteType>();

        }
    }
}