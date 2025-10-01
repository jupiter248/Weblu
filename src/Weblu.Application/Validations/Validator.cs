using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Domain.Errors.Commons;

namespace Weblu.Application.Validations
{
    public static class Validator
    {
        public static void ValidateAndThrow<T>(T instance, IValidator<T> validator)
        {
            // or get your registered validator

            var result = validator.Validate(instance);

            if (!result.IsValid)
            {
                // Group errors by property and get localized error messages
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new Exceptions.ValidationException("VALIDATION_ERROR", CommonErrorCodes.ValidationError, errors);
            }
        }
    }
}