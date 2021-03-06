﻿using System.Threading.Tasks;

namespace Common.Core.Cqrs
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
