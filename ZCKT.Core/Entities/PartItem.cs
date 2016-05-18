using ZCKT.Infrastructure;

namespace ZCKT.Entities
{
    public class PartItem : IEntity<string>
    {
        public string Id { get; set; }

        /// <summary>
        /// 父项ID
        /// </summary>
        public string ParentID { get; set; }

        public string CodeID { get; set; }

        /// <summary>
        /// 国外码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 国内码
        /// </summary>
        public string HomCode { get; set; }

        /// <summary>
        /// 公司码
        /// </summary>
        public string CompCode { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 零件价格
        /// </summary>
        public decimal? PartPrice { get; set; }

        /// <summary>
        /// 子项数量
        /// </summary>
        public int ChildCount { get; set; }
    }
}
