using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectDAA1.Controllers
{
    public class AuthSVController : Controller
    {
        protected MyDatabaseEntities9 db = new MyDatabaseEntities9();
        protected int? idsv;
        // GET: AuthSV
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];

            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "" }));
            }
            else
            {
                if (session.Nhom == "Quản trị viên")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "Admin" }));
                }
                else if (session.Nhom == "Giảng viên")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
                }
                idsv = session.idsv;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}