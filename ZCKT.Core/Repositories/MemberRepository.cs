using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCKT.Entities;
using ZCKT.Infrastructure;

namespace ZCKT.Repositories
{
    public class MemberRepository : BaseRepository
    {
        public MemberRepository(DBHelperProvider dbHelperProvider)
            : base(dbHelperProvider)
        { }

        /// <summary>
        /// 取得用户负责的产品
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>产品名称列表</returns>
        public string[] GetUserProductTypes(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");

            string sql =
                @"SELECT pt.TypeName
                FROM [dbo].[ProductType] pt
	                INNER JOIN [dbo].[Member] m on CharIndex(',' + Cast(pt.TypeID as nvarchar(50)) + ',',m.Remark) > 0 
                WHERE m.UserName={0}";
            var cmd = DBContext.CreateCommand(sql, username.Trim());
            string[] products = this.DBContext.ExecuteArray<string>(cmd);
            return products;
        }
    }
}
