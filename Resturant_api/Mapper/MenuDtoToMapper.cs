using AutoMapper;
using Resturant_api.Model.Domain;
using Resturant_api.Model.DTO;

namespace Resturant_api.Mapper
{
    public class MenuDtoToMapper : Profile
    {
        public MenuDtoToMapper()
        {
            CreateMap<Menu, MenuDto>().ReverseMap(); 
            CreateMap<AddMenuRequestDto, Menu>().ReverseMap();
            CreateMap<UpdateMenuRequestDto, Menu>().ReverseMap();
        }
    }
}
