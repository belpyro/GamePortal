using GamePortal.Web.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace GamePortal.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ValidateModelAttribute());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //REST
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}