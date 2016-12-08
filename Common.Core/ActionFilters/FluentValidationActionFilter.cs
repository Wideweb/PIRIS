using Common.Core.Extensions;
using Common.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Common.Core.ActionFilters
{
    public class FluentValidationActionFilter : ActionFilterAttribute
    {
        private readonly IServiceProvider serviceProvider;

        public FluentValidationActionFilter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments)
            {
                var validator = FluentValidationHelper.GetModelValidator(argument.Value, serviceProvider);
                if(validator != null)
                {
                    var validationResult = await validator.ValidateAsync(argument.Value);
                    context.ModelState.AddValidationFailures(validationResult.Errors);
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            await next();
        }
    }
}
