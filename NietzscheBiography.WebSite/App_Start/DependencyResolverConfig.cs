using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using NietzscheBiography.Domain.Models;
using NietzscheBiography.WebSite.Infrastructure.Dal;
using NietzscheBiography.WebSite.Infrastructure.Services;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NietzscheBiography.WebSite
{
    public static class DependencyResolverConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<NietzscheBiographyDbContext>().InstancePerHttpRequest();

            // Standard repositories
            builder.Register(c => c.Resolve<NietzscheBiographyDbContext>().Participants)
                .As<IQueryable<Participant>>().InstancePerDependency();

            builder.Register(c => c.Resolve<NietzscheBiographyDbContext>().Events)
                .As<IQueryable<Event>>().InstancePerDependency();

            builder.Register(c => c.Resolve<NietzscheBiographyDbContext>().EventTypes)
                .As<IQueryable<EventType>>().InstancePerDependency();

            builder.Register(c => c.Resolve<NietzscheBiographyDbContext>().Locations)
                .As<IQueryable<Location>>().InstancePerDependency();

            builder.Register(c => c.Resolve<NietzscheBiographyDbContext>().MediaItems)
                .As<IQueryable<MediaItem>>().InstancePerDependency();

            // Services
            builder.RegisterType<EnglishPluralizationService>()
                .As<IPluralizationService>();

            builder.RegisterType<SentenceTemplateFileRepository>()
                .As<ISentenceTemplateRepository>()
                .WithParameter(
                    "templatesFilePath",
                    HttpRuntime.AppDomainAppPath.TrimEnd('/', '\\') + "/App_Data/EventTitleTemplates.xml")
                .SingleInstance();

            builder.RegisterType<SentenceSynthesizer>()
                .As<ISentenceSynthesizer>().SingleInstance();

            var container = builder.Build();

            // Set the dependency resolver for MVC.
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);

            // Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }
    }
}