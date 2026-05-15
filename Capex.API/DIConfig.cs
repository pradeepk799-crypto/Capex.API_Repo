using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Capex.Business.Common.DBLogger;
using Capex.Business.Services;
using Capex.Models.Common;
using Capex.Utilities.Common;
using SimpleInjector;
using Microsoft.AspNetCore.Identity;
using Capex.Models.RequestModel;
using Capex.Models.RequestModel.Masters;

namespace Capex.API
{
    public static class DIConfig
    {
        /// <summary>
        /// ConfigureServicesConfig.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="container">The container.</param>
        public static void ConfigureServicesConfig(IServiceCollection services, Container container)
        {
            services.AddSimpleInjector(container, options =>
            {
                // AddAspNetCore() wraps web requests in a Simple Injector scope.
                options.AddAspNetCore()
                    // Ensure activation of a specific framework type to be created by
                    // Simple Injector instead of the built-in configuration system.
                    .AddControllerActivation();
                //.AddViewComponentActivation();
            });
        }

        /// <summary>
        /// ConfigureConfig.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="container">The container.</param>
        public static void ConfigureConfig(IApplicationBuilder app, Container container)
        {
            DIConfig.InitializeContainer(app, container);
            app.UseSimpleInjector(container);
            // Always verify the container
            container.Verify();
        }

        /// <summary>
        /// Initializes the container.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="container">The container.</param>
        private static void InitializeContainer(IApplicationBuilder app, Container container)
        {
            // Add application services. For instance:
            var repositoryAssemblyBLL = typeof(User).Assembly;
            var registrationsBLL =
                from type in repositoryAssemblyBLL.GetExportedTypes()
                where type.Namespace.Equals("Capex.Business.Services")
                from service in type.GetInterfaces()
                select new { service, implementation = type };

           

            foreach (var reg in registrationsBLL)
            {
                container.Register(reg.service, reg.implementation, Lifestyle.Singleton);
            }
            container.Register(typeof(ILoggerProvider), typeof(DbLoggerProvider), Lifestyle.Singleton);

            var repositoryAssembly = typeof(Capex.Infrastructure.Services.User).Assembly;

            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace.Equals("Capex.Infrastructure.Services")
                from service in type.GetInterfaces()
                select new { service, implementation = type };

            foreach (var reg in registrations)
            {
                container.Register(reg.service, reg.implementation, Lifestyle.Singleton);
            }
            container.Register<SMSNotification>(Lifestyle.Singleton);
            container.Register<EmailNotification>(Lifestyle.Singleton);
            container.Register<WhatsAppNotification>(Lifestyle.Singleton);
            //container.Register<IPasswordHasher<TokenRequestModel>, PasswordHasher<TokenRequestModel>>(Lifestyle.Singleton);
            //container.Register<IPasswordHasher<DDORequestModel>, PasswordHasher<DDORequestModel>>(Lifestyle.Singleton);
            container.Register<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>(Lifestyle.Singleton);
        }
    }
}
