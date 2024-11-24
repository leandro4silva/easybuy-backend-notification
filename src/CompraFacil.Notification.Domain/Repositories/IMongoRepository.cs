namespace CompraFacil.Notification.Domain.Repositories;

public interface IMongoRepository<T>
{
    Task<T> GetAsync(string @event);
}
