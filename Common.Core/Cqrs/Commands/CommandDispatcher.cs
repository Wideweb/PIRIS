using System;
using System.Threading.Tasks;
using Autofac;
namespace Common.Core.Cqrs
{
    public class CommandDispatcher: ICommandDispatcher
    {
        private readonly IComponentContext context;

        public CommandDispatcher(IComponentContext context)
        {
            this.context = context;
        }

        public Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
        {
            return context.Resolve<ICommandHandler<TCommand, TResult>>().HandleAsync(command);
        }
    }
}
