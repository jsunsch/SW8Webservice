using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Excolo.Architecture.Core.NHibernate;
using TACO.Model.IoC;
using TACO.Model.Utilities;

namespace TACO
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        private IWindsorContainer container;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "CreatePOI", // Route name
                "CreatePOI/{name}/{lon}/{lat}/{description}", // URL with parameters
                new { controller = "Home", action = "CreatePOI", description = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "GetTexts", // Route name
                "GetTexts/{id}", // URL with parameters
                new { controller = "Home", action = "GetTexts" } // Parameter defaults
            );
            routes.MapRoute(
                "GetPoint", // Route name
                "GetPoint/{id}", // URL with parameters
                new { controller = "Home", action = "GetPoint" } // Parameter defaults
            );
            routes.MapRoute(
                "KNearestPOI", // Route name
                "KNearestPOI/{lon}/{lat}/{number}/{radius}", // URL with parameters
                new { controller = "Home", action = "KNearestPOI", radius = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "Default", // Route name
                "{action}/{id}", // URL with parameters
                new { controller = "Home", action = "AllPoints", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();


    
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);


            CastleWindsorInitializer.InitializeWindsorContainer();
            CastleWindsorInitializer.InitializeControllerFactory(
                                            new Action<IControllerFactory>(ControllerBuilder.Current.SetControllerFactory));
        }


        protected void Application_End()
        {
            container.Dispose();
        }


        protected void Application_BeginRequest(object sender, EventArgs args)
        {
            NHibernateInitializer.Instance.InitializeNHibernateOnce(
                                            () => NHibernateSession.Init(DatabaseConfig.GetConfig()));
        }
    }
}