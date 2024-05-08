using Event.Domain.Entities;
using Event.Infraestructure.Commons.Bases.Request;
using Event.Infraestructure.Commons.Bases.Response;

namespace Event.Infraestructure.Persistences.Interfaces
{
    public interface IOccurrenceRepository : IGenericRepository<Occurrence>
    {
        Task<BaseEntityResponse<Occurrence>> ListOccurrences(BaseFilterRequest filters);
    }
}
