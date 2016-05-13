using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ZCKT.WebApi;

namespace ZCKT.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //认证MesageHandler
            config.MessageHandlers.Add(new AuthMessageHandler());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
