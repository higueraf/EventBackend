using Event.Application.Commons.bases;
using Event.Application.Dtos.User.Request;
using Event.Application.Dtos.User.Response;
using Event.Infraestructure.Commons.Bases.Request;
using Event.Infraestructure.Commons.Bases.Response;

namespace Event.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto);
        Task<BaseResponse<UserTokenResponseDto>> GenerateToken(TokenRequestDto requestDto);

        Task<BaseResponse<BaseEntityResponse<UserResponseDto>>> ListUsers(BaseFilterRequest filters);
        Task<BaseResponse<IEnumerable<UserSelectResponseDto>>> ListSelectUsers();
        Task<BaseResponse<UserResponseDto>> UserById(int userId);
        Task<BaseResponse<bool>> CreateUser(UserRequestDto requestDto);
        Task<BaseResponse<bool>> UpdateUser(int userId, UserRequestDto requestDto);
        Task<BaseResponse<bool>> DeleteUser(int userId);

    }
}
