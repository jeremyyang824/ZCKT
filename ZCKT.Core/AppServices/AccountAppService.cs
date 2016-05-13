using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCKT.DTOs;
using ZCKT.Infrastructure;
using ZCKT.ValueObjects;

namespace ZCKT.AppServices
{
    public class AccountAppService : BaseAppService
    {
        /// <summary>
        /// 登录
        /// </summary>
        public MembershipContext Login(LoginInputDto loginDto)
        {
            return new MembershipContext(loginDto.Username)
            {
                IsAuthenticated = true
            };
        }
    }
}
