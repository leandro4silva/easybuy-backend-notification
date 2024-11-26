using CompraFacil.Notification.Domain.Entities;

namespace CompraFacil.Notification.Domain.Repositories;

public interface IMailRepository
{
    Task AddAsync(EmailTemplate emailTemplate, CancellationToken cancellationToken);

    Task<EmailTemplate> GetTemplate(string @event);
}
