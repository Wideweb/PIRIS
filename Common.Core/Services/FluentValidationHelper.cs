using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Reflection;

namespace Common.Core.Services
{
    public static class FluentValidationHelper
    {
        public static IValidator GetModelValidator<T>(T modelObject)
        {
            var validator = modelObject.GetType().GetTypeInfo().GetCustomAttribute<ValidatorAttribute>();
            return validator == null ? null : (IValidator)Activator.CreateInstance(validator.ValidatorType);
        }
    }
}
