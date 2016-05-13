using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCKT.Entities;
using ZCKT.Infrastructure;

namespace ZCKT.Repositories
{
    public class PartItemRepository : BaseRepository
    {
        public PartItemRepository(DBHelperProvider dbHelperProvider)
            : base(dbHelperProvider)
        { }

        public PartItem GetByKey(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            string sql = $"SELECT {this.getEntityFields()} FROM dbo.TotalData WHERE [id]={{0}} ORDER BY RowID";
            var cmd = DBContext.CreateCommand(sql, id);
            var entity = this.DBContext.ExecuteObject<PartItem>(cmd);
            return entity;
        }

        public List<PartItem> GetRootItems(IEnumerable<string> products)
        {
            string sql = $"SELECT {this.getEntityFields()} FROM dbo.TotalData WHERE ParentID=0";
            if (products != null)
                sql += $" AND content IN ({getProductsConditionFragment(products)})";
            sql += " ORDER BY RowID";
            return this.DBContext.ExecuteList<PartItem>(sql);
        }

        public List<PartItem> GetComponentItems(string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
                throw new ArgumentNullException("parentId");

            string sql = $"SELECT {this.getEntityFields()} FROM dbo.TotalData WHERE ParentID={{0}} ORDER BY RowID";
            var cmd = DBContext.CreateCommand(sql, parentId);
            return this.DBContext.ExecuteList<PartItem>(cmd);
        }


        private string getProductsConditionFragment(IEnumerable<string> products)
        {
            if (products == null)
                throw new ArgumentNullException("products");

            return $"'{string.Join("','", products.Select(p => p.Trim()).Where(p => p != string.Empty))}'";
        }

        private string getEntityFields()
        {
            return "[id] AS Id,[parentid] AS ParentID,[CodeID] AS CodeID,[content] AS ItemCode,[HomCode] AS HomCode,[CompCode] AS CompCode,[PartName] AS PartName,[PartPrice] AS PartPrice";
        }
    }
}
