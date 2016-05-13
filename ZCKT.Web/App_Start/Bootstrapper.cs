using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ZCKT.Web
{
    public class Bootstrapper
    {
        /// <summary>
        /// 在应用程序启动时运行的代码
        /// </summary>
        public static void Run()
        {
            AreaRegistration.RegisterAllAreas();

            // Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);

            //Configure AutoMapper
            AutoMapperConfig.Configure();

            //注册MVC路由
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //注册WebAPI路由、MessageHandler
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.EnsureInitialized();

            //注册Http Content（js、css、image）
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}