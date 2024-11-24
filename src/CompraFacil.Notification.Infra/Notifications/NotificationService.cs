using CompraFacil.Notification.Infra.Notifications.Abstraction;

namespace CompraFacil.Notification.Infra.Notifications;

public class NotificationService : INotificationService
{
    private readonly List<ErroResponse> _erros = new();

    public void Adicionar(ErroResponse erroResponse) =>
        _erros.Add(erroResponse);

    public bool ExisteNotificacao() =>
        _erros.ElementAtOrDefault(0) != null;

    public List<ErroResponse> ObterTodos() => _erros;
}
