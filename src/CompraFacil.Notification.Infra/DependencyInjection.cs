using CompraFacil.Notification.Infra.Configurations;
using CompraFacil.Notification.Infra.Notifications;
using CompraFacil.Notification.Infra.Notifications.Abstraction;
using CompraFacil.Notification.Infra.SendGrid;
using CompraFacil.Notification.Infra.SendGrid.Abstraction;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CompraFacil.Notification.Infra;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }

    public static IServiceCollection AddMailService(this IServiceCollection services, AppConfiguration appConfiguration)
    {
        services.AddSingleton<MailConfiguration>(m => appConfiguration.Mail!);

        services.AddSendGrid(sp => sp.ApiKey = appConfiguration.Mail?.SendGridApiKey);

        services.AddTransient<ISendMailService, SendMailService>();

        return services;
    }
}
