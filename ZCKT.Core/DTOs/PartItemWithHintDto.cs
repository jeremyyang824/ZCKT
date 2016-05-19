using System;

namespace ZCKT.DTOs
{
    public class PartItemWithHintDto : PartItemDto
    {
        /// <summary>
        /// 从BOM根到目标节点的Id索引
        /// </summary>
        public string[] IdHint { get; set; }

        /// <summary>
        /// 从BOM根到目标节点的ItemCode索引
        /// </summary>
        public string[] ItemCodeHint { get; set; }
    }
}
