using Event.Application.Commons.bases;
using Event.Application.Dtos.Occurrence.Request;
using Event.Application.Dtos.Occurrence.Response;
using Event.Application.Dtos.User.Response;
using Event.Infraestructure.Commons.Bases.Request;
using Event.Infraestructure.Commons.Bases.Response;

namespace Event.Application.Interfaces
{
    public interface IOccurrenceApplication
    {
        Task<BaseResponse<BaseEntityResponse<OccurrenceResponseDto>>> ListOccurrences(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<OccurrenceSelectResponseDto>>> ListSelectOccurrences();
        Task<BaseResponse<OccurrenceResponseDto>> OccurrenceById(int occurrenceId);
        Task<BaseResponse<bool>> CreateOccurrence(OccurrenceRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateOccurrence(int occurrenceId, OccurrenceRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteOccurrence(int occurrenceId);
    }
}
