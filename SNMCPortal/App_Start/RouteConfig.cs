using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SNMCPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //   name: "MPRegisterQuadrant",
            //   url: "MortgagePortal/{controller}/RegisterQuadrant/{quadrant}/{targetUrl}",
            //   defaults: new { controller = "Home", action = "RegisterQuadrant", quadrant = "", targetUrl = "" }
            //);
            routes.MapRoute(
                name: "MPHomeless",
                url: "MortgagePortal/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "MPDefault",
                url: "MortgagePortal/{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
              name: "RegisterQuadrant",
              url: "{controller}/RegisterQuadrant/{quadrant}/{targetUrl}",
              defaults: new { controller = "Home", action="RegisterQuadrant", quadrant = "", targetUrl = "" }
            );
            routes.MapRoute(
                name: "Homeless",
                url: "{action}",
                defaults: new { controller="Home", action = "Index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}