using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RefuseCollect
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
       routeTemplate: "api/{controller}/{id}",
       defaults: new
       {
           id = RouteParameter.Optional,
           pareaname = RouteParameter.Optional,
           pareaid = RouteParameter.Optional,
           platitude = RouteParameter.Optional,
           plongitude = RouteParameter.Optional,
           postrefuse = RouteParameter.Optional,
           putrefuse = RouteParameter.Optional


       }
   );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{abid}/{pareaid}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    pareaname = RouteParameter.Optional,
                    pareaid = RouteParameter.Optional,
                    platitude = RouteParameter.Optional,
                    plongitude = RouteParameter.Optional,
                    postrefuse = RouteParameter.Optional,
                    putrefuse = RouteParameter.Optional


                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi3",
                routeTemplate: "api/{controller}/{anid}/{aggtype}/{aggquery}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    pareaname = RouteParameter.Optional,
                    pareaid = RouteParameter.Optional,
                    platitude = RouteParameter.Optional,
                    plongitude = RouteParameter.Optional,
                    postrefuse = RouteParameter.Optional,
                    putrefuse = RouteParameter.Optional


                }
            );

        }
    }
}
