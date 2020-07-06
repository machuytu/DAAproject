using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectDAA1.Areas.Admin.Controllers
{
    public class AdminAuthController : Controller
    {
        // GET: Admin/AdminAuth

        protected MyDatabaseEntities9 db = new MyDatabaseEntities9();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "" }));
            }
            else if (session.Nhom != "Quản trị viên")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}