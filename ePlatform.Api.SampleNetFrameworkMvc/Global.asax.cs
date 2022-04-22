using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.Mvc;
using ePlatform.Api;
using Microsoft.Extensions.DependencyInjection;

namespace ePlatform.Api.SampleNetFrameworkMvc
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var services = new ServiceCollection();

            //Use this method to add invoice clients
            services.AddePlatformClients(clientOptions =>
            {
                clientOptions.InvoiceServiceUrl = "https://efaturaservicetest.isim360.com";
                clientOptions.ApiKey = "testapikey";
            });

            var providerFactory = new AutofacServiceProviderFactory();
            //to populate your container
            ContainerBuilder builder = providerFactory.CreateBuilder(services);

            // MVC - Register your MVC controllers.
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);

            // MVC - OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(WebApiApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // MVC - OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // MVC - Set Autofac as dependency resolver.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Standard MVC setup:

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
