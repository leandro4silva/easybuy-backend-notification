namespace CompraFacil.Notification.Domain.Events;

public class CustomerCreated
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }
    
    public string? Email { get; set; }
}
