using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApiThrottle;

namespace FundaHousing
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
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                QuotaExceededResponseCode= HttpStatusCode.ServiceUnavailable,
                QuotaExceededMessage = "Ooops! Please try after some time",
                // Generic rate limit applied to ALL APIs
                Policy = new ThrottlePolicy(perMinute: 100)
                {
                    IpThrottling = true,
                    ClientThrottling = true,
                    EndpointThrottling = true,
                },
                Repository = new CacheRepository()
            });
           
        }
    }
}
