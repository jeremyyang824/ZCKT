﻿using System;

namespace ZCKT.DTOs
{
    public class PartItemDto
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

        public string FullName
        {
            get { return $"{(CodeID == "000000000" ? "" : CodeID + "_")}{ItemCode}"; }
        }
    }
}
