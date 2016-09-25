using System.Threading.Tasks;

namespace Common.Core.Cqrs
{
    public interface ICommandDispatcher
    {
        Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
    }
}
