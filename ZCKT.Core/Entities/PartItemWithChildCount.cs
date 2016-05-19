using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCKT.Entities
{
    public class PartItemWithChildCount : PartItem
    {
        /// <summary>
        /// 子项数量
        /// </summary>
        public int ChildCount { get; set; }
    }
}
