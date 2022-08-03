using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using Castle.MicroKernel.Registration;
using CoMute.Web.App_Start;
using System.Web.Routing;

namespace CoMute.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var connectionStringSettings = ConfigurationManager.ConnectionStrings["LendingLibraryWebContext"];
            //DBMigrationsRunner runner = new DBMigrationsRunner(connectionStringSettings.ConnectionString);
            //runner.MigrateToLatest();
            Boostrap();
        }
        private IWindsorContainer Boostrap()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());
            container.Register(Component.For<IWindsorContainer>()
                .Instance(container));

            container.Register(
               Component
                   .For<IControllerFactory>()
                   .ImplementedBy<WindsorControllerFactory>()
                   .LifeStyle.Singleton
               );

            container.Register(Classes.FromThisAssembly()
              .BasedOn<IController>()
              .LifestyleTransient());
            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestyleScoped()
            );

            GlobalConfiguration.Configuration.DependencyResolver = new App_Start.DependencyResolver(container.Kernel);
            SetControllerFactory(container);

            return container;
        }

        private static void SetControllerFactory(WindsorContainer container)
        {
            var windsorControllerFactory = container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(windsorControllerFactory);
        }
    }
}

