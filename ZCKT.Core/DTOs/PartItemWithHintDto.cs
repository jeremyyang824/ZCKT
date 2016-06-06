using System;

namespace ZCKT.DTOs
{
    public class PartItemWithHintDto : PartItemDto
    {
        public HintDto[] Hints { get; set; }
    }
}
