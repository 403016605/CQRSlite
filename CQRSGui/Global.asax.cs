﻿using System.Web.Mvc;
using System.Web.Routing;
using CQRSGui.Tools;
using Microsoft.Practices.ServiceLocation;
using SimpleCQRS;
using StructureMap;

namespace CQRSGui
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            SetupStructureMap();
            RegisterHandlers(ServiceLocator.Current);

        }

        private void SetupStructureMap() {
            var registry = new StructureMapRegistry();
            var container = new Container(registry);
            var structureMapServiceLocator = new StructureMapServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => structureMapServiceLocator);
            var locator = new StructureMapServiceLocatorControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(locator);
        }

        private void RegisterHandlers(IServiceLocator serviceLocator)
        {
            var registerer = new BusRegisterer();
            registerer.Register(serviceLocator,typeof(Handles<>));
        }

    }
}