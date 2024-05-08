using AutoMapper;
using Event.Application.Dtos.User.Request;
using Event.Application.Dtos.User.Response;
using Event.Application.Dtos.User.Request;
using Event.Application.Dtos.User.Response;
using Event.Domain.Entities;
using Event.Infraestructure.Commons.Bases.Response;
using Event.Utils.Static;

namespace Event.Application.Mappers
{
    public class UserMappingsProfile : Profile
    {
        public UserMappingsProfile() 
        {
            CreateMap<UserRequestDto, User>();
            CreateMap<TokenRequestDto, User>();
            CreateMap<User, UserResponseDto>()
                .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
                .ForMember(c => c.StateUser, c => c.MapFrom(s => s.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"));
            CreateMap<BaseEntityResponse<User>, BaseEntityResponse<UserResponseDto>>()
                .ReverseMap();
            CreateMap<User, UserSelectResponseDto>()
                .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
                .ReverseMap();


        }
    }
}
