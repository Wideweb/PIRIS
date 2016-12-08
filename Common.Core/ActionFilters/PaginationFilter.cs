using Common.Core.Constants;
using Common.Core.Models;
using EternityFramework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Core.ActionFilters
{
    public static class ActionFilterContextExtensions
    {
        public static T GetResultValue<T>(this IActionResult actionResult)
        {
            return (T)actionResult.GetType().GetTypeInfo().GetProperty("Value").GetValue(actionResult);
        }

        public static int GetStatusCode(this IActionResult actionResult)
        {
            return (int)actionResult.GetType().GetTypeInfo().GetProperty("StatusCode").GetValue(actionResult);
        }

        public static void SetResultValue(this IActionResult actionResult, object value)
        {
            actionResult.GetType().GetTypeInfo().GetProperty("Value").SetValue(actionResult, value);
        }
    }

    public class PaginationFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            if (context.Result.GetStatusCode() != 400)
            {
                var data = context.Result.GetResultValue<IQueryable>();

                if (headers.ContainsKey(HttpRequestHeaders.Pagination) &&
                    (typeof(IQueryable).IsAssignableFrom(data.GetType())))
                {
                    var paginationHeader = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<PaginationHeader>(headers[HttpRequestHeaders.Pagination]);

                    var pagedExpression = ApplyPagination(paginationHeader, data.Expression);
                    context.Result.SetResultValue(data.Provider.CreateQuery(pagedExpression));
                }
            }

            base.OnResultExecuting(context);
        }

        private Expression ApplyPagination(PaginationHeader paginationHeader, Expression expression)
        {
            if (!string.IsNullOrEmpty(paginationHeader.OrderBy))
            {
                var parameter = Expression.Parameter(TypeSystem.GetElementType(expression.Type), "it");
                var propertyToOrderByExpression = Expression.Property(parameter, paginationHeader.OrderBy);
                var lambda = Expression.Lambda(propertyToOrderByExpression, parameter);
                
                expression = Expression.Call(
                    typeof(Queryable),
                    "OrderBy",
                    new Type[] { TypeSystem.GetElementType(expression.Type), propertyToOrderByExpression.Type },
                    expression,
                    lambda);
            }

            if (paginationHeader.Skip.HasValue)
            {
                expression = Expression.Call(
                    null,
                    typeof(Queryable).GetMethod("Skip").MakeGenericMethod(TypeSystem.GetElementType(expression.Type)),
                    expression,
                    Expression.Constant(paginationHeader.Skip));
            }

            if (paginationHeader.Take.HasValue)
            {
                expression = Expression.Call(
                    null,
                    typeof(Queryable).GetMethod("Take").MakeGenericMethod(TypeSystem.GetElementType(expression.Type)),
                    expression,
                    Expression.Constant(paginationHeader.Take));
            }

            return expression;
        }
    }
}
