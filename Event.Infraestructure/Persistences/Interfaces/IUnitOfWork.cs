namespace Event.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Declaración de de nuestras interfaces a nivel de repository
        IOccurrenceRepository Occurrence {  get; }
        IUserRepository User { get; }
        void SaveChangers();
        Task SaveChangersAsync();

    }
}
