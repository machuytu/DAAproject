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
                 name: "lop",
                 url: "lop",
                 defaults: new { controller = "Teacher", action = "dsLop", id = UrlParameter.Optional },
                 namespaces: new[] { "ProjectDAA1.Controllers" }
             );

            routes.MapRoute(
                 name: "lopcn",
                 url: "lopcn",
                 defaults: new { controller = "Teacher", action = "dsLopCN", id = UrlParameter.Optional },
                 namespaces: new[] { "ProjectDAA1.Controllers" }
             );

            routes.MapRoute(
                 name: "svlopcn",
                 url: "svlopcn/{id}",
                 defaults: new { controller = "Teacher", action = "dsSVLopCN", id = UrlParameter.Optional },
                 namespaces: new[] { "ProjectDAA1.Controllers" }
             );

            routes.MapRoute(
             name: "Đăng ký học phần",
             url: "dkhp",
             defaults: new { controller = "DKHP", action = "GetDKHP", id = UrlParameter.Optional },
             namespaces: new[] { "ProjectDAA1.Controllers" }
         );

            routes.MapRoute(
                name: "Huỷ đăng ký học phần",
                url: "huydkhp",
                defaults: new { controller = "DKHP", action = "GetHuyDKHP", id = UrlParameter.Optional },
                namespaces: new[] { "ProjectDAA1.Controllers" }
            );

            routes.MapRoute(
                name: "kết quả học tập sinh viên",
                url: "sinhvien/kqht",
                defaults: new { controller = "DKHP", action = "GetKQHT", id = UrlParameter.Optional },
                namespaces: new[] { "ProjectDAA1.Controllers" }
            );

            routes.MapRoute(
                name: "thời khoá biểu sinh viên",
                url: "sinhvien/tkb",
                defaults: new { controller = "DKHP", action = "GetTKB", id = UrlParameter.Optional },
                namespaces: new[] { "ProjectDAA1.Controllers" }
            );

            routes.MapRoute(
                name: "thời khoá biểu giảng viên",
                url: "giangvien/tkb",
                defaults: new { controller = "Teacher", action = "GetTKB", id = UrlParameter.Optional },
                namespaces: new[] { "ProjectDAA1.Controllers" }
            );

            routes.MapRoute(
                 name: "svlop",
                 url: "svlop/{id}",
                 defaults: new { controller = "Teacher", action = "dsSVLop", id = UrlParameter.Optional },
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
