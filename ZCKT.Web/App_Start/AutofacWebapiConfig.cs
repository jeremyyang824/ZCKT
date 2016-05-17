using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ZCKT.AppServices;
using ZCKT.Infrastructure;
using ZCKT.Repositories;

namespace ZCKT.Web
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container { get; private set; }

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterSerivces(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            //设置WebAPI Resolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //设置MVC Resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static IContainer RegisterSerivces(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //Infrastructure
            //builder.RegisterType(typeof(DBHelperProvider))
            //    .AsSelf()
            //    .WithParameter("connectionStringName", "ZCKT")
            //    .Named<DBHelperProvider>("ZCKT")
            //    .InstancePerRequest();

            builder.RegisterInstance(new DBHelperProvider("ZCKT"))
                .Named<DBHelperProvider>("ZCKT");

            builder.RegisterInstance(new DBHelperProvider("ZCJX"))
                .Named<DBHelperProvider>("ZCJX");

            //builder.RegisterType(typeof (UnitOfWorkManager))
            //    .As<IUnitOfWorkManager>()
            //    .InstancePerRequest();

            //Repositories
            builder.Register(ctx => new PartItemRepository(ctx.ResolveNamed<DBHelperProvider>("ZCKT")))
                .AsSelf()
                .InstancePerRequest();

            builder.Register(ctx => new MemberRepository(ctx.ResolveNamed<DBHelperProvider>("ZCJX")))
                .AsSelf()
                .InstancePerRequest();


            //AppService
            builder.RegisterType<ItemAppService>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<AccountAppService>()
                .AsSelf()
                .InstancePerRequest();

            Container = builder.Build();
            return Container;
        }
    }
}