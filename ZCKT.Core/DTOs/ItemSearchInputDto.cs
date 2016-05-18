using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCKT.Infrastructure;
using ZCKT.Validators;

namespace ZCKT.DTOs
{
    public class ItemSearchInputDto : BaseValidatableObject<ItemSearchInputDtoValidator>
    {
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
    }
}
