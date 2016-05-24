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

        /// <summary>
        /// 取得BOM根物料（限定产品）
        /// </summary>
        /// <param name="products">限定产品</param>
        public List<PartItemWithChildCount> GetRootItems(IEnumerable<string> products)
        {
            string sql = $"SELECT {this.getEntityFields()},{this.getChildCountField()} FROM dbo.TotalData WHERE ParentID=0";
            if (products != null)
                sql += $" AND Content IN ({this.getProductsConditionFragment(products)})";
            sql += " ORDER BY RowID";
            return this.DBContext.ExecuteList<PartItemWithChildCount>(sql);
        }

        /// <summary>
        /// 获取BOM单层子项
        /// </summary>
        public List<PartItemWithChildCount> GetComponentItems(string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
                throw new ArgumentNullException("parentId");

            string sql = $"SELECT {this.getEntityFields()},{this.getChildCountField()} FROM dbo.TotalData WHERE ParentID={{0}} ORDER BY RowID";
            var cmd = DBContext.CreateCommand(sql, parentId);
            return this.DBContext.ExecuteList<PartItemWithChildCount>(cmd);
        }


        /// <summary>
        /// 根据国外码模糊查找
        /// </summary>
        /// <param name="products">限定产品</param>
        /// <param name="contentLike">国外码</param>
        /// <returns></returns>
        public List<PartItemWithHint> FindItemsByContent(IEnumerable<string> products, string contentLike)
        {
            if (string.IsNullOrWhiteSpace(contentLike))
                throw new ArgumentNullException("contentLike");

            string sqlFilter = $"AND Content LIKE {{0}}";
            var cmd = DBContext.CreateCommand(findItemsSQL(products, sqlFilter), "%" + contentLike + "%");
            return this.DBContext.ExecuteList<PartItemWithHint>(cmd);
        }

        /// <summary>
        /// 根据国内码模糊查找
        /// </summary>
        /// <param name="products">限定产品</param>
        /// <param name="homcodeLike">国内码</param>
        /// <returns></returns>
        public List<PartItemWithHint> FindItemsByHomcode(IEnumerable<string> products, string homcodeLike)
        {
            if (string.IsNullOrWhiteSpace(homcodeLike))
                throw new ArgumentNullException("homcodeLike");

            string sqlFilter = $"AND HomCode LIKE {{0}}";
            var cmd = DBContext.CreateCommand(findItemsSQL(products, sqlFilter), "%" + homcodeLike + "%");
            return this.DBContext.ExecuteList<PartItemWithHint>(cmd);
        }

        /// <summary>
        /// 根据公司码模糊查找
        /// </summary>
        /// <param name="products">限定产品</param>
        /// <param name="compcodeLike">公司码</param>
        /// <returns></returns>
        public List<PartItemWithHint> FindItemsByCompcode(IEnumerable<string> products, string compcodeLike)
        {

            if (string.IsNullOrWhiteSpace(compcodeLike))
                throw new ArgumentNullException("compcodeLike");

            string sqlFilter = $"AND CompCode LIKE {{0}}";
            var cmd = DBContext.CreateCommand(findItemsSQL(products, sqlFilter), "%" + compcodeLike + "%");
            return this.DBContext.ExecuteList<PartItemWithHint>(cmd);
        }

        /// <summary>
        /// 根据物料名模糊查找
        /// </summary>
        /// <param name="products">限定产品</param>
        /// <param name="partnameLike">物料名</param>
        /// <returns></returns>
        public List<PartItemWithHint> FindItemsByPartname(IEnumerable<string> products, string partnameLike)
        {
            if (string.IsNullOrWhiteSpace(partnameLike))
                throw new ArgumentNullException("partnameLike");

            string sqlFilter = $"AND PartName LIKE {{0}}";
            var cmd = DBContext.CreateCommand(findItemsSQL(products, sqlFilter), "%" + partnameLike + "%");
            return this.DBContext.ExecuteList<PartItemWithHint>(cmd);
        }



        //SQL语句，查找物料(PartItemWithHint), 限制50个查询结果
        private string findItemsSQL(IEnumerable<string> products, string sqlFilter)
        {
            //string sqlFilter = "AND Content = {{0}}";
            string sqlFindItems =
                $@"WITH cte
                AS(
                    SELECT ID, ParentID, CodeID, Content as ItemCode, HomCode, CompCode, PartName, PartPrice, 1 as Lv, ID as TargetID, CAST(RTRIM(ID) AS VARCHAR(500)) as [Path]
                    FROM dbo.TotalData WHERE 1 = 1 {sqlFilter}
                    UNION ALL
                    SELECT p.ID, p.ParentID, p.CodeID, p.Content as ItemCode, p.HomCode, p.CompCode, p.PartName, p.PartPrice, s.Lv + 1, s.TargetID, CAST(s.[Path] + '.' + RTRIM(p.ID) AS VARCHAR(500))
                    FROM cte s
                        INNER JOIN dbo.TotalData p ON s.ParentID = p.ID
                )
                SELECT TOP 50 {this.getEntityFields()},{this.getHintFields()}
                FROM dbo.TotalData
                WHERE ID IN (
                    SELECT inr.TargetID FROM cte as inr
                    WHERE inr.ID IN(SELECT ID FROM dbo.TotalData WHERE ParentID = 0 AND Content IN ({this.getProductsConditionFragment(products)}))
                )
                ORDER BY ID";
            return sqlFindItems;
        }


        //SQL片段，所属产品
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

        private string getChildCountField()
        {
            return "(SELECT COUNT(1) FROM dbo.TotalData _cd WHERE _cd.parentid=dbo.TotalData.Id) AS ChildCount";
        }

        private string getHintFields()
        {
            //从BOM树根到目标物料（不含该目标物料）的路径索引
            return "STUFF((SELECT '|'+l.ID FROM cte as l WHERE l.TargetID=dbo.TotalData.id AND l.ID<>l.TargetID ORDER BY l.TargetID,l.Lv DESC FOR XML PATH('')),1,1,'') AS IdHint,"
                   + "STUFF((SELECT ' | '+l.ItemCode FROM cte as l WHERE l.TargetID=dbo.TotalData.id AND l.ID<>l.TargetID ORDER BY l.TargetID,l.Lv DESC FOR XML PATH('')),1,3,'') AS ItemCodeHint";
        }
    }
}
