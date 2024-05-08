using Event.Infraestructure.Persistences.Contexts;
using Event.Infraestructure.Persistences.Interfaces;


namespace Event.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventContext _context;
        public IOccurrenceRepository Occurrence { get; private set; }
        public IUserRepository User { get; private set; }

        public UnitOfWork(EventContext context)
        {
            _context = context;
            Occurrence = new OccurrenceRepository(_context);
            User = new UserRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChangers()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangersAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
