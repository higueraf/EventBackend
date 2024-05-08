using AutoMapper;
using Event.Application.Commons.bases;
using Event.Application.Dtos.Occurrence.Request;
using Event.Application.Dtos.Occurrence.Response;
using Event.Application.Interfaces;
using Event.Application.Validators.Occurrence;
using Event.Domain.Entities;
using Event.Infraestructure.Commons.Bases.Request;
using Event.Infraestructure.Commons.Bases.Response;
using Event.Infraestructure.Persistences.Interfaces;
using Event.Utils.Static;

namespace Event.Application.Services
{
    public class OccurrenceApplication : IOccurrenceApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly OccurrenceValidator _validationRules;
        public OccurrenceApplication(IUnitOfWork unitOfWork, IMapper mapper, OccurrenceValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }


        public async Task<BaseResponse<BaseEntityResponse<OccurrenceResponseDto>>> ListOccurrences(BaseFilterRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<OccurrenceResponseDto>>();
            var occurrences= await _unitOfWork.Occurrence.ListOccurrences(filters);
            if (occurrences is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<OccurrenceResponseDto>>(occurrences);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            } else
            {
                response.IsSuccess = false;
                response.Message= ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<OccurrenceSelectResponseDto>>> ListSelectOccurrences()
        {
            var response = new BaseResponse<IEnumerable<OccurrenceSelectResponseDto>>();
            var occurrences= await _unitOfWork.Occurrence.GetAllAsync();
            if (occurrences is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<OccurrenceSelectResponseDto>>(occurrences);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<OccurrenceResponseDto>> OccurrenceById(int occurrenceId)
        {
            var response = new BaseResponse<OccurrenceResponseDto>();
            var occurrence = await _unitOfWork.Occurrence.GetByIdAsync(occurrenceId);
            if (occurrence is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<OccurrenceResponseDto>(occurrence);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> CreateOccurrence(OccurrenceRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);
            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }
            var occurrence = _mapper.Map<Occurrence>(requestDto);
            response.Data = await _unitOfWork.Occurrence.CreateAsync(occurrence);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_CREATE;

            }
            else
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }
        public async Task<BaseResponse<bool>> UpdateOccurrence(int occurrenceId, OccurrenceRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var occurrenceEdit = await OccurrenceById(occurrenceId);
            if (occurrenceEdit is null) { 
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            var occurrence = _mapper.Map<Occurrence>(requestDto);
            occurrence.Id = occurrenceId;
            response.Data = await _unitOfWork.Occurrence.UpdateAsync(occurrence);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

            }
            else
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        

        public async Task<BaseResponse<bool>> DeleteOccurrence(int occurrenceId)
        {
            var response = new BaseResponse<bool>();
            var occurrenceEdit = await OccurrenceById(occurrenceId);
            if (occurrenceEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            response.Data = await _unitOfWork.Occurrence.DeleteAsync(occurrenceId);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;

            }
            else
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }
        
    }
}
