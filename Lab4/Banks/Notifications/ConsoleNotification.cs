namespace Banks.Notifications;

public class ConsoleNotification : INotification
{
    public void Notify(string message)
    {
        Console.WriteLine(message);
    }
}