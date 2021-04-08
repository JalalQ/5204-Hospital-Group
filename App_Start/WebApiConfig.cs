using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GeraldtonHospital_v1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                //when you set up the controller, make sure to add the route or you will get ERRORS!!!!!!
                //==== need to add /{action} attribute to the routeTemplate ======
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
