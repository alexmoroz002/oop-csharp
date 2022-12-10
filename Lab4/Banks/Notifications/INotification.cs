namespace Banks.Notifications;

public interface INotification
{
    void Notify(string message);
}