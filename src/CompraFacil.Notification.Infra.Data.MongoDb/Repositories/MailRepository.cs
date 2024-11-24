using CompraFacil.Notification.Domain.Entities;
using CompraFacil.Notification.Domain.Repositories;

namespace CompraFacil.Notification.Infra.Data.MongoDb.Repositories;

public class MailRepository : IMailRepository
{
    private readonly IMongoRepository<EmailTemplate> _mongoRepository;

    public MailRepository(IMongoRepository<EmailTemplate> mongoRepository)
    {
        _mongoRepository = mongoRepository;
    }

    public async Task<EmailTemplate> GetTemplate(string @event)
    {
        return await _mongoRepository.GetAsync(@event);
    }
}
