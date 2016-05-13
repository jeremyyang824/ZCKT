using System;
using System.Collections.Generic;

namespace ZCKT.ValueObjects
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class MembershipContext
    {
        public bool IsAuthenticated { get; set; }

        public int? UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string[] Roles { get; set; }

        public MembershipContext(string userName)
        {
            this.IsAuthenticated = false;
            this.UserName = userName.Trim();
            this.Roles = new string[] { };
        }
    }
}
