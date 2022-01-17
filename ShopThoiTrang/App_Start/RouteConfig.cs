using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopThoiTrang
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Contact",
      url: "Index",
      defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
      );
            //      routes.MapRoute(
            //name: "Contact",
            //url: "Contact",
            //defaults: new { controller = "Site", action = "contact", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
        name: "men",
        url: "men",
        defaults: new { controller = "Site", action = "ProductMen", id = UrlParameter.Optional }
        );
            routes.MapRoute(
        name: "women",
        url: "women",
        defaults: new { controller = "Site", action = "ProductWomen", id = UrlParameter.Optional }
        );
            routes.MapRoute(
          name: "product",
          url: "product",
          defaults: new { controller = "Site", action = "AllProduct", id = UrlParameter.Optional }
          );
            routes.MapRoute(
            name: "post",
            url: "post",
            defaults: new { controller = "Site", action = "getAllPost", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "thanh-toan",
              url: "thanh-toan",
              defaults: new { controller = "Checkout", action = "index", id = UrlParameter.Optional }
              );
            routes.MapRoute(
               name: "huy thanh toan online",
               url: "cancel-order",
               defaults: new { controller = "Checkout", action = "cancel_order", id = UrlParameter.Optional }
               );
            routes.MapRoute(
           name: "thanh toan thanh cong",
           url: "confirm-orderPaymentOnline",
           defaults: new { controller = "Checkout", action = "confirm_orderPaymentOnline", id = UrlParameter.Optional }
           );
            routes.MapRoute(
           name: "huy thanh toan online momo",
           url: "cancel-order-momo",
           defaults: new { controller = "Checkout", action = "cancel_order_momo", id = UrlParameter.Optional }
           );

            routes.MapRoute(
           name: "thanh toan thanh cong momo",
           url: "confirm-orderPaymentOnline-momo",
           defaults: new { controller = "Checkout", action = "confirm_orderPaymentOnline_momo", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                 name: "gio-hang",
                 url: "gio-hang",
                 defaults: new { controller = "Cart", action = "index", id = UrlParameter.Optional }
                 );
            routes.MapRoute(
                 name: "thongtinkhachhang",
                 url: "thong-tin-khach-hang",
                 defaults: new { controller = "Customer", action = "index", id = UrlParameter.Optional }
                 );
            routes.MapRoute(
                  name: "login - register",
                  url: "dang-nhap-dang-ky",
                  defaults: new { controller = "AuthCus", action = "LoginOrRegister", id = UrlParameter.Optional }
                  );
            routes.MapRoute(
                    name: "search product",
                    url: "tim-kiem-san-pham",
                    defaults: new { controller = "Site", action = "Sreach", id = UrlParameter.Optional }
                    );
            routes.MapRoute(
                    name: "slug",
                    url: "{slug}",
                    defaults: new { controller = "Site", action = "index", id = UrlParameter.Optional }
                    );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
