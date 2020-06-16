using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectDAA1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "Login",
                 url: "login",
                 defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional },
                 namespaces: new[] { "ProjectDAA1.Controllers" }
             );
            routes.MapRoute(
                 name: "Manage Class",
                 url: "lops/{idgv}",
                 defaults: new { controller = "Teacher", action = "Getclass", id = UrlParameter.Optional },
                 namespaces: new[] { "ProjectDAA1.Controllers" }
             );

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "ProjectDAA1.Controllers" }
             );

        }
    }
}
