using AutoMapper;
using Event.Application.Dtos.Occurrence.Request;
using Event.Application.Dtos.Occurrence.Response;
using Event.Application.Dtos.User.Response;
using Event.Domain.Entities;
using Event.Infraestructure.Commons.Bases.Response;
using Event.Utils.Static;


namespace POS.Application.Mappers
{
    public class OccurrenceMappingsProfile : Profile
    {
        public OccurrenceMappingsProfile()
        {
            CreateMap<Occurrence, OccurrenceResponseDto>()
                .ForMember(x => x.OccurrenceId, x => x.MapFrom(y => y.Id))
                .ForMember(c => c.StateOccurrence, c => c.MapFrom(s => s.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"));
            CreateMap<BaseEntityResponse<Occurrence>, BaseEntityResponse<OccurrenceResponseDto>>()
                .ReverseMap();
            CreateMap<OccurrenceRequestDto, Occurrence>();
            CreateMap<Occurrence, OccurrenceSelectResponseDto>()
                .ForMember(x => x.OccurrenceId, x => x.MapFrom(y => y.Id))
                .ReverseMap();


        }
    }
}
