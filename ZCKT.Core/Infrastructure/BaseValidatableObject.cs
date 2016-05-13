using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;

namespace ZCKT.Infrastructure
{
    /// <summary>
    /// 可验证对象基类
    /// </summary>
    /// <typeparam name="TValidator">验证器</typeparam>
    public abstract class BaseValidatableObject<TValidator> : IValidatableObject
        where TValidator : IValidator, new()
    {
        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            TValidator validator = new TValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
