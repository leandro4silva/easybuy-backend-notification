namespace CompraFacil.Notification.Domain.Repositories;

public interface IMongoRepository<T>
{
    Task AddAsync(T entity);

    Task<T> GetAsync(string @event);
}
