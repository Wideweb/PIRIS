using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Core.ActionFilters;
using Common.Core.DataAccess;

namespace Common.Core.Configurations
{
    public static class CommonConfig
    {
        public static void AddCommonTools(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddOptions();
            services.Configure<DbQueryOptions>(configuration.GetSection("DbQueryOptions"));
            services.AddAutoMapper();
            services.AddMvc(cfg =>
            {
                cfg.Filters.Add(typeof(FluentValidationActionFilter));
                cfg.Filters.Add(typeof(CustomExceptionFilterAttribute));
            });
        }
    }
}
