using System.Threading.Tasks;

namespace Common.Core.Cqrs
{
    public interface ICommandHandler<TCommand, TResult> where TCommand: ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
