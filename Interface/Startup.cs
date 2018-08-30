using System;
using System.Threading;
using Infrastructure;
using Infrastructure.UnitOfWork;
using Interface.Ninject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;

namespace Interface
{
    public class Startup
    {
        #region Props
        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();

        private IKernel Kernel { get; set; }

        private object Resolve(Type type) => Kernel.Get(type);

        private object RequestScope(IContext context) => scopeProvider.Value;

        private sealed class Scope : DisposableObject { }

        public IConfigurationRoot Configuration { get; }
        #endregion

        #region Public
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureNinject(services);
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            this.Kernel = this.RegisterApplicationComponents(app);
            app.UseMvc();
        }
        #endregion

        #region Private

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            var kernel = new StandardKernel();
            
            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }
            
            kernel.Bind<UnitOfWork>().ToSelf().InThreadScope().WithConstructorArgument("connectionString", Configuration.GetConnectionString("DefaultBase"));

            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }

        public void ConfigureNinject(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);
        }
        #endregion
    }
}
