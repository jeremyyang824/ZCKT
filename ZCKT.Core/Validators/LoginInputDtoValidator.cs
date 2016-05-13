using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using ZCKT.DTOs;

namespace ZCKT.Validators
{
    public class LoginInputDtoValidator : AbstractValidator<LoginInputDto>
    {
        public LoginInputDtoValidator()
        {
            this.RuleFor(r => r.Username).NotEmpty()
                .WithMessage("Invalid username");

            //this.RuleFor(r => r.Password).NotEmpty()
            //    .WithMessage("Invalid password");
        }
    }
}
