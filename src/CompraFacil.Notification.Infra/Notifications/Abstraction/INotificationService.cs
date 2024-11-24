namespace CompraFacil.Notification.Infra.Notifications.Abstraction;

public interface INotificationService
{
    bool ExisteNotificacao();

    void Adicionar(ErroResponse erro);

    List<ErroResponse> ObterTodos();
}
