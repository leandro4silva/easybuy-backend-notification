namespace CompraFacil.Notification.Infra.SendGrid.Abstraction;

public interface ISendMailService
{
    Task SendAsync(string subject, string content, string toEmail, string toName);
}
