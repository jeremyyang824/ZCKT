using System;

namespace ZCKT.DTOs
{
    public class PartItemWithChildCountDto : PartItemDto
    {
        /// <summary>
        /// 子项数量
        /// </summary>
        public int ChildCount { get; set; }
    }
}
