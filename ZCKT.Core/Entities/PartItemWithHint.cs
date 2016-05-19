using System;

namespace ZCKT.Entities
{
    public class PartItemWithHint : PartItem
    {
        /// <summary>
        /// 从BOM根到目标节点的Id索引
        /// </summary>
        public string IdHint { get; set; }

        /// <summary>
        /// 从BOM根到目标节点的ItemCode索引
        /// </summary>
        public string ItemCodeHint { get; set; }
    }
}
