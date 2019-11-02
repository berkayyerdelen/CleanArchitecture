using AutoMapper;
using Core.Comman.Infrastructure.AutoMapper;

namespace Application.UnitTests.Common
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}