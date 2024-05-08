

using Microsoft.EntityFrameworkCore;
using Event.Domain.Entities;
using Event.Infraestructure.Commons.Bases.Request;
using Event.Infraestructure.Commons.Bases.Response;
using Event.Infraestructure.Persistences.Contexts;
using Event.Infraestructure.Persistences.Interfaces;

namespace Event.Infraestructure.Persistences.Repositories
{
    public class OccurrenceRepository : GenericRepository<Occurrence>, IOccurrenceRepository
    {
        public OccurrenceRepository(EventContext context) : base(context) { }
        
        public async Task<BaseEntityResponse<Occurrence>> ListOccurrences(BaseFilterRequest filters)
        {
            var response = new BaseEntityResponse<Occurrence>();
            var occurrences = GetEntityQuery(c=> c.DeleteBy == null && c.DeleteDate == null);
            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch(filters.NumFilter)
                {
                    case 1:
                        occurrences = occurrences.Where(c => c.Description!.Contains(filters.TextFilter)); 
                        break;
                    case 2:
                        occurrences = occurrences.Where(c => c.Description!.Contains(filters.TextFilter));
                        break;

                }
            }
            if (filters.StateFilter is not null)
            {
                occurrences = occurrences.Where(category => category.State!.Equals(filters.StateFilter));
            }
            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                occurrences = occurrences.Where(c => c.CreateDate >= Convert.ToDateTime(filters.StartDate) && c.CreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));

            }
            if (filters.Sort is not null) filters.Sort = "Id";
            response.TotalRecords = await occurrences.CountAsync();
            response.Items = await Ordering(filters, occurrences, !(bool)filters.Download).ToListAsync();
            return response;
        }

    }
}
