using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ZCKT.DTOs;
using ZCKT.Entities;

namespace ZCKT
{
    public class DomainToViewModelMappingProfile
    {
        public void Register(IProfileExpression mapperConfig)
        {
            mapperConfig.CreateMap<PartItem, PartItemDto>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id.Trim()))
                .ForMember(vm => vm.ParentID, map => map.MapFrom(m => m.ParentID == null ? null : m.ParentID.Trim()))
                .ForMember(vm => vm.CodeID, map => map.MapFrom(m => m.CodeID == null ? null : m.CodeID.Trim()))
                .ForMember(vm => vm.ItemCode, map => map.MapFrom(m => m.ItemCode == null ? null : m.ItemCode.Trim()))
                .ForMember(vm => vm.HomCode, map => map.MapFrom(m => m.HomCode == null ? null : m.HomCode.Trim()))
                .ForMember(vm => vm.CompCode, map => map.MapFrom(m => m.CompCode == null ? null : m.CompCode.Trim()))
                .ForMember(vm => vm.PartName, map => map.MapFrom(m => m.PartName == null ? null : m.PartName.Trim()));

            mapperConfig.CreateMap<PartItemWithChildCount, PartItemWithChildCountDto>()
                .IncludeBase<PartItem, PartItemDto>();

            mapperConfig.CreateMap<PartItemWithHint, PartItemWithHintDto>()
                .IncludeBase<PartItem, PartItemDto>()
                .ForMember(vm => vm.IdHint, map => map.MapFrom(m => m.IdHint.Split('|').Select(i => i.Trim())))
                .ForMember(vm => vm.ItemCodeHint, map => map.MapFrom(m => m.ItemCodeHint.Split('|').Select(i => i.Trim())));
        }
    }
}