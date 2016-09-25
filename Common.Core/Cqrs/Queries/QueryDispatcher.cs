using Autofac;
using System.Threading.Tasks;

namespace Common.Core.Cqrs
{
    public class QueryDispatcher : IQueryDispatcher
    { 
        private readonly IComponentContext componentContex;
        public QueryDispatcher(IComponentContext componentContex)
        {
            this.componentContex = componentContex;
        }

        public Task<TResult> Dispatch<TQuery, TResult>(TQuery query) where TQuery: IQuery<TResult>
        {
            return componentContex.Resolve<IQueryHandler<TQuery, TResult>>().HandleAsync(query);
        }
    }
}
