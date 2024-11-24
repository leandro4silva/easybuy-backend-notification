using CompraFacil.Notification.Domain.Entities;

namespace CompraFacil.Notification.Domain.Repositories;

public interface IMailRepository
{
    Task<EmailTemplate> GetTemplate(string @event);
}
