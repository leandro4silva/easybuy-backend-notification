using CompraFacil.Notification.Application.Handlers.v1.CreateTemplate;
using CompraFacil.Notification.Application.Mappers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CompraFacil.Notification.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region MediatR

        services.AddMediatR(typeof(CreateEmailTemplateHandler));
        services.AddAutoMapperProfiles();

        #endregion

        return services;
    }

    private static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(EmailTemplateProfile));
        return services;
    }
}
