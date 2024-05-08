using Event.Domain.Entities;
using Event.Infraestructure.Commons.Bases.Request;
using Event.Infraestructure.Commons.Bases.Response;

namespace Event.Infraestructure.Persistences.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> AccountByEmail(string email);
        Task<BaseEntityResponse<User>> ListUsers(BaseFilterRequest filters);
    }
}
