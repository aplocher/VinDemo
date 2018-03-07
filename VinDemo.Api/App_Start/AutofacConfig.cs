using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using VinDemo.DataAccess.SqlServer.Context;
using VinDemo.Domain.Interfaces;
using VinDemo.Model.Services;
using VinDemo.Repository;

namespace VinDemo.Api
{
    public class AutofacConfig
    {
#pragma warning disable DV0001 // Invalid dependency

        public static void Register()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<MemberService>().As<IMemberService>();
            builder.RegisterType<VinDemoDbContext>().As<IVinDemoDbContext>();
            builder.RegisterWebApiFilterProvider(config);
            //builder.RegisterType<ItemMustNotExistAttribute>().PropertiesAutowired();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

#pragma warning restore DV0001 // Invalid dependency
    }
}