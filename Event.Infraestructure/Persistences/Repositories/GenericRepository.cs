using Microsoft.EntityFrameworkCore;
using Event.Domain.Entities;
using Event.Infraestructure.Commons.Bases.Request;
using Event.Infraestructure.Helpers;
using Event.Infraestructure.Persistences.Contexts;
using Event.Infraestructure.Persistences.Interfaces;
using Event.Utils.Static;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Event.Infraestructure.Persistences.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly EventContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(EventContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity
                .Where(x => x.State.Equals((int)StateTypes.Active) && x.DeleteBy == null && x.DeleteDate == null)
                .AsNoTracking()
                .ToListAsync();
            return getAll;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return getById!;
        }

       

        public async Task<bool> CreateAsync(T entity)
        {
            entity.CreateBy = 1;
            entity.CreateDate = DateTime.Now;
            await _context.AddAsync(entity);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            entity.UpdateBy = 1;
            entity.UpdateDate = DateTime.Now;
            _context.Update(entity);
            _context.Entry(entity).Property(c => c.CreateBy).IsModified = false;
            _context.Entry(entity).Property(c => c.CreateDate).IsModified = false;
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            entity.DeleteBy = 1;
            entity.DeleteDate = DateTime.Now;
            _context.Update(entity);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;

        }
        public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;
            Console.WriteLine("filter higueraf", filter);

            if (filter is not null) query = query.Where(filter);

            return query;
        }


        public IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");
            if (pagination) queryDto = queryDto.Paginate(request);
            return queryDto;
        }
    }
}
