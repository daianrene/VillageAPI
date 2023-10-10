using AutoMapper;
using VillageAPI.Models;
using VillageAPI.Models.Dto;

namespace VillageAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Village, VillageDto>();
            CreateMap<VillageDto, Village>();
            //CreateMap<Village, VillageDto>().ReverseMap();
        }
    }
}
