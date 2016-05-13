using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZCKT.DTOs;
using ZCKT.Entities;

namespace ZCKT
{
    public class DomainToViewModelMappingProfile
    {
        public void Register(IProfileExpression mapperConfig)
        {
            mapperConfig.CreateMap<PartItem, PartItemDto>();
        }
    }
}