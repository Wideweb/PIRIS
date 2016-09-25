using AutoMapper;
using Common.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(AssemblyHelper.GetReferencedAssemblies());
            });

            services.AddSingleton(sp => _mapperConfiguration.CreateMapper());
        }
    }
}
