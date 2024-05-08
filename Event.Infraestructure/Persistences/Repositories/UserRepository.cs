

using Event.Domain.Entities;
using Event.Infraestructure.Commons.Bases.Request;
using Event.Infraestructure.Commons.Bases.Response;
using Event.Infraestructure.Persistences.Contexts;
using Event.Infraestructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Event.Infraestructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly EventContext _context;
        public UserRepository(EventContext context) : base(context)
        {
            _context = context;

        }

        async Task<User> IUserRepository.AccountByEmail(string email)
        {
            var account = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email!.Equals(email));
            return account!;
        }

        public async Task<BaseEntityResponse<User>> ListUsers(BaseFilterRequest filters)
        {
            var response = new BaseEntityResponse<User>();
            var users = GetEntityQuery(c => c.DeleteBy == null && c.DeleteDate == null);
            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        users = users.Where(c => c.UserName!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        users = users.Where(c => c.UserName!.Contains(filters.TextFilter));
                        break;

                }
            }
            if (filters.StateFilter is not null)
            {
                users = users.Where(category => category.State!.Equals(filters.StateFilter));
            }
            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                users = users.Where(c => c.CreateDate >= Convert.ToDateTime(filters.StartDate) && c.CreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));

            }
            if (filters.Sort is not null) filters.Sort = "Id";
            response.TotalRecords = await users.CountAsync();
            response.Items = await Ordering(filters, users, !(bool)filters.Download).ToListAsync();
            return response;
        }
    }
}
