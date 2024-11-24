namespace CompraFacil.Notification.Domain.Entities.Abstraction;

public interface IEntityBase
{
    Guid Id { get; }

    string? Event { get; }
}
