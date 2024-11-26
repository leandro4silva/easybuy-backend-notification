using AutoMapper;
using CompraFacil.Notification.Application.Helpers;
using CompraFacil.Notification.Domain.Entities;
using CompraFacil.Notification.Domain.Repositories;
using CompraFacil.Notification.Infra.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompraFacil.Notification.Application.Handlers.v1.CreateTemplate;

public sealed class CreateEmailTemplateHandler : IRequestHandler<CreateEmailTemplateCommand, CreateEmailTemplateResult>
{
    private readonly IMailRepository _mailRepository;
    private readonly ILogger<CreateEmailTemplateHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public CreateEmailTemplateHandler(
        IMailRepository mailRepository,
        ILogger<CreateEmailTemplateHandler> logger,
        INotificationService notificationService, 
        IMapper mapper
    )
    {
        _mailRepository = mailRepository;
        _logger = logger;
        _notificationService = notificationService;
        _mapper = mapper;
    }

    public async Task<CreateEmailTemplateResult> Handle(CreateEmailTemplateCommand request, CancellationToken cancellationToken)
    {
        var emailTemplate = _mapper.Map<EmailTemplate>(request);

        try
        {
            await _mailRepository.AddAsync(emailTemplate, cancellationToken);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido no cadastro de usuario";
            NotificationHelper.Notificar(ex, msg, _notificationService, _logger);
        }

        return _mapper.Map<CreateEmailTemplateResult>(emailTemplate);
    }
}
