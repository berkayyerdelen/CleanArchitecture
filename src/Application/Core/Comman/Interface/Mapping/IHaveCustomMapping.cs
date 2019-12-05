using AutoMapper;

namespace Core.Comman.Interface.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}