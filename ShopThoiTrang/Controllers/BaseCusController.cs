using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopThoiTrang.Controllers
{
    public class BaseCusController : Controller
    {
        // GET: BaseCus
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Muser sessionUser = (Muser)Session[Common.CommonConstants.CUSTOMER_SESSION];
            if (sessionUser == null)
            {

                RouteValueDictionary route = new RouteValueDictionary(new { Controller = "AuthCus", Action = "LoginOrRegister" });
                Message.set_flash("Please login", "danger");
                filterContext.Result = new RedirectToRouteResult(route);
                return;
            }
        }
    }
}