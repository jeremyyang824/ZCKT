using AutoMapper;

namespace ZCKT.Infrastructure
{
    public interface IMapperRegistrar
    {
        void Register(IProfileExpression mapperConfig);
    }
}
