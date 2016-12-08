using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Core.ActionFilters;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Common.Core.Configurations
{
    public static class CommonConfig
    {
        public static void AddCommonTools(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var policy = new CorsPolicy();

            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;
            
            services.AddCors(t => t.AddPolicy("allow_all", policy));

            services.AddOptions();
            services.AddLocalization();
            services.AddAutoMapper();
            services.AddMvc(cfg =>
            {
                cfg.Filters.Add(typeof(FluentValidationActionFilter));
                cfg.Filters.Add(typeof(CustomExceptionFilterAttribute));
            });
            
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new[]
                    {
                        new CultureInfo("en"),
                        new CultureInfo("ru"),
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "ru", uiCulture: "ru");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });
        }

        public static IApplicationBuilder UseCommonTools(this IApplicationBuilder app)
        {
            app.UseCors("allow_all");

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseMvc();

            return app;
        }
    }
}
