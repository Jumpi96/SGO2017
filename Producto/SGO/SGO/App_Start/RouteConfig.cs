using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGO
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DetailsObra",
                url: "{controller}/{action}/{id}/{rubro}/{subrubro}/{item}/{subitem}/{enPesos}",
                defaults: new { controller = "Obras", action = "Details", id = 0, rubro = 0, subrubro = 0, item = 0, subitem = 0, enPesos = true }
            );
        }
    }
}
