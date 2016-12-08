using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Common.Core.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddValidationFailure(this ModelStateDictionary modelState, ValidationFailure validationFailure)
        {
            modelState.AddModelError(validationFailure.PropertyName, validationFailure.ErrorMessage);
        }

        public static void AddValidationFailures(this ModelStateDictionary modelState, IEnumerable<ValidationFailure> validationFailures)
        {
            foreach (var failure in validationFailures)
            {
                modelState.AddValidationFailure(failure);
            }
        }
    }
}
