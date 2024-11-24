using CompraFacil.Notification.Domain.Entities.Abstraction;

namespace CompraFacil.Notification.Domain.Entities;

public class EmailTemplate : IEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Subject { get; set; }

    public string? Content { get; set; }

    public string? Event { get; set; }
}
