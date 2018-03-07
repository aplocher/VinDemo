using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using VinDemo.DataAccess.SqlServer.Context;
using VinDemo.Domain.Interfaces;
using VinDemo.Model.Services;
using VinDemo.Repository;

namespace VinDemo
{
    public class AutofacConfig
    {
#pragma warning disable DV0001 // Invalid dependency

        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<MemberService>().As<IMemberService>();
            builder.RegisterType<VinDemoDbContext>().As<IVinDemoDbContext>();
            builder.RegisterFilterProvider();
            //builder.RegisterType<ItemMustNotExistAttribute>()
            //    .OnActivated(x => x.Instance.UnitOfWork = x.Context.Resolve<UnitOfWork>());
            ////builder.RegisterType<ItemMustNotExistAttribute>().PropertiesAutowired();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

#pragma warning restore DV0001 // Invalid dependency
    }
}