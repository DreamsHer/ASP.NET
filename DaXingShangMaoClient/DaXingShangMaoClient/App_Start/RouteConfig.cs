using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DaXingShangMaoClient
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ZhuMian",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ZhuMian", action = "ZhuMian", id = UrlParameter.Optional }
            );
        }
    }
}
