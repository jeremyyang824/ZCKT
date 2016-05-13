using AutoMapper;

namespace ZCKT.Web
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                new DomainToViewModelMappingProfile().Register(cfg);
            });
        }
    }
}