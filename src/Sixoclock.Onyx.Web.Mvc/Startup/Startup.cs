using System;
using Abp.AspNetCore;
using Abp.Authorization;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Hangfire;
using Abp.IdentityServer4;
using Abp.Runtime.Security;
using Castle.Facilities.Logging;
using Hangfire;
using IdentityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Authorization.Roles;
using Sixoclock.Onyx.Authorization.Users;
using Sixoclock.Onyx.Configuration;
using Sixoclock.Onyx.Identity;
using Sixoclock.Onyx.MultiTenancy;
using Sixoclock.Onyx.Web.Authentication.JwtBearer;
using Sixoclock.Onyx.Web.Resources;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using Sixoclock.Onyx.API.JsonClient.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Sixoclock.Onyx.Web.IdentityServer;
using StackExchange.Profiling.Storage;

#if FEATURE_SIGNALR
using Owin;
using Abp.Owin;
using Sixoclock.Onyx.Web.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
#endif

namespace Sixoclock.Onyx.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults

                // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
                options.RouteBasePath = "/profiler";

               

                // (Optional) Control which SQL formatter to use, InlineFormatter is the default
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();

               
            }).AddEntityFramework();
            //MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            var identityBuilder = IdentityRegistrar.Register(services);

            //Identity server
            if (bool.Parse(_appConfiguration["IdentityServer:IsEnabled"]))
            {
                IdentityServerRegistrar.Register(services, _appConfiguration);
            }

            AuthConfigurer.Configure(services, _appConfiguration);

            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new Info { Title = "Onyx API", Version = "v1" });
            //    options.DocInclusionPredicate((docName, description) => true);
            //});

            //Recaptcha
            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = _appConfiguration["Recaptcha:SiteKey"],
                SecretKey = _appConfiguration["Recaptcha:SecretKey"]
            });
            //Register Json client between Chargepoint and CS
            services.AddDeviceHub();
            //Hangfire (Enable to use Hangfire instead of default job manager)
            //services.AddHangfire(config =>
            //{
            //    config.UseSqlServerStorage(_appConfiguration.GetConnectionString("Default"));
            //});

            services.AddScoped<IWebResourceManager, WebResourceManager>();

            //Configure Abp and Dependency Injection
            return services.AddAbp<OnyxWebMvcModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiniProfiler();

            //Initializes ABP framework.
            app.UseAbp(options =>
            {
                options.UseAbpRequestLocalization = false; //used below: UseAbpRequestLocalization
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("~/Error?statusCode={0}");
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();

            if (bool.Parse(_appConfiguration["Authentication:JwtBearer:IsEnabled"]))
            {
                app.UseJwtTokenMiddleware();
            }

            if (bool.Parse(_appConfiguration["IdentityServer:IsEnabled"]))
            {
                app.UseJwtTokenMiddleware("IdentityBearer");
                app.UseIdentityServer();
            }

            app.UseStaticFiles();
            app.UseAbpRequestLocalization();

#if FEATURE_SIGNALR
            //Integrate to OWIN
            app.UseAppBuilder(ConfigureOwinServices);
#endif

            //Hangfire dashboard & server (Enable to use Hangfire instead of default job manager)
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new AbpHangfireAuthorizationFilter(AppPermissions.Pages_Administration_HangfireDashboard)  }
            //});
            //app.UseHangfireServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            //app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Onyx API V1");
            //}); //URL: /swagger
        }

#if FEATURE_SIGNALR
        private static void ConfigureOwinServices(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(typeof(IAssemblyLocator), () => new SignalRAssemblyLocator());
            app.Properties["host.AppName"] = "Onyx";

            app.UseAbp();

            app.MapSignalR();
        }
#endif
    }
}
