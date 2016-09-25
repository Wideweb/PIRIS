using Common.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Common.Core.ActionFilters
{
    public class FluentValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var validationErrors = filterContext.ActionArguments.Values
                .Select(it => new { Argument = it, Validator = FluentValidationHelper.GetModelValidator(it) })
                .Where(it => it.Validator != null)
                .SelectMany(it => it.Validator.Validate(it.Argument).Errors);

            if (!validationErrors.Any())
            {
                return;
            }
            
            foreach(var error in validationErrors)
            {
                filterContext.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            
            filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
        }
    }
}
