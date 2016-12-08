using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace Common.Core.ActionFilters
{
    public class LanguageActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string culture = context.RouteData.Values["culture"].ToString();
            
            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            base.OnActionExecuting(context);
        }
    }
}
