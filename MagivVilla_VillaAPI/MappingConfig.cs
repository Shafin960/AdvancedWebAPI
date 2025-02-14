using AutoMapper;
using MagivVilla_VillaAPI.DTO;
using MagivVilla_VillaAPI.Models;

namespace MagivVilla_VillaAPI;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Villa, VillaDTO>();
        CreateMap<VillaDTO, Villa>();

        CreateMap<Villa, VillaCreateDTO>();
        CreateMap<VillaCreateDTO, Villa>();
        
        CreateMap<Villa, VillaUpdateDTO>();
        CreateMap<VillaUpdateDTO, Villa>();
    }
}