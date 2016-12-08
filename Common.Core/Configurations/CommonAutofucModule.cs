using System.Linq;
using Autofac;
using Common.Core.Cqrs;
using Common.Core.Services;
using Autofac.Features.ResolveAnything;

namespace Common.Core.Configurations
{
    public class CommonAutofucModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            BindAssemblyTypes(builder);

            base.Load(builder);
        }

        protected virtual void BindAssemblyTypes(ContainerBuilder builder)
        {
            var assemblies = AssemblyHelper.GetReferencedAssemblies();

            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommandHandler<,>));
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IQueryHandler<,>));

            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
            
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Resolver") ||
                            t.Name.EndsWith("Selector") ||
                            t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
        }
    }
}
