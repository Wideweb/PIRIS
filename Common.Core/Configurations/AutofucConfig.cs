using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Configurations
{
    public static class AutofucConfig
    {
        public static IContainer BuildApplicationContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(AssemblyHelper.GetReferencedAssemblies());
            builder.Populate(services);
            return builder.Build();
        }

        public static AutofacServiceProvider CreateAutofacServiceProvider(this IServiceCollection services)
        {
            return new AutofacServiceProvider(services.BuildApplicationContainer());
        }
    }
}
