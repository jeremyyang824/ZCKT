﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZCKT.DTOs;

namespace ZCKT.Validators
{
    public class ItemSearchInputDtoValidator : AbstractValidator<ItemSearchInputDto>
    {
        public ItemSearchInputDtoValidator()
        {
            this.RuleFor(r => r.SearchKey).NotEmpty()
                .WithMessage("SearchKey required!");

            this.RuleFor(r => r.SearchValue).NotEmpty()
                .Must(r => r.Length >= 3)
                .WithMessage("查询内容必须大等于3个字符!");
        }
    }
}
