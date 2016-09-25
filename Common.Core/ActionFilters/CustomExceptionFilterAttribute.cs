using Common.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Common.Core.ActionFilters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, Action<ExceptionContext>> exceptionHanlders;

        public CustomExceptionFilterAttribute()
        {
            exceptionHanlders = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(PirisObjectNotFoundException), HandleNotFoundException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            if (exceptionHanlders.ContainsKey(exceptionType))
            {
                exceptionHanlders[exceptionType](context);
            }
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}
