using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZCKT.Infrastructure;
using ZCKT.Validators;

namespace ZCKT.DTOs
{
    public class LoginInputDto : BaseValidatableObject<LoginInputDtoValidator>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}