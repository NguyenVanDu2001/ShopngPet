using ShopThoiTrang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopThoiTrang.Common
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        public string RoleID { set; get; } = string.Empty;
        public bool IsAdmin { get; set; } = true;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (Userlogin)HttpContext.Current.Session[Common.CommonConstants.USER_SESSION];

            if (session == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(RoleID))
            {
                if (session.Roles.Contains(this.RoleID) && (session.GroupID == CommonConstants.ADMIN_GROUP || session.GroupID == "Customer"))
                {
                    return true;
                }
                return false;
            }else
            {
                if ((session.GroupID == CommonConstants.ADMIN_GROUP || session.GroupID == "Customer"))
                {
                    return true;
                }
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //neu chua dang nhap
            if (HttpContext.Current.Session[Common.CommonConstants.USER_SESSION] == null)
            {
                RouteValueDictionary route = new RouteValueDictionary(new { Controller = "AuthCus", Action = "LoginOrRegister" });
                filterContext.Result = new RedirectToRouteResult(route);
                return;
            }
            // neu da dang nhap ma khong co quyen truy cap
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            };
        }

    }
}