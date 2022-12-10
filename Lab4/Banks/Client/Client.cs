using Banks.Accounts.Interfaces;
using Banks.Client.Builder;
using Banks.Exceptions;
using Banks.Notifications;

namespace Banks.Client;

public class Client : IClient
{
    private int? _passport;
    private string _address;
    private List<IBankAccount> _accounts;

    internal Client(IBuilder builder)
    {
        _accounts = new List<IBankAccount>();
        IsSuspicious = true;
        Name = builder.Name;
        Surname = builder.Surname;
        Address = builder.Address;
        Passport = builder.Passport;
    }

    public string Name { get; init; }
    public string Surname { get; init; }
    public INotification Notification { get; set; } = new ConsoleNotification();

    public int? Passport
    {
        get => _passport;
        set
        {
            if (_passport != null)
                throw ClientException.PassportAlreadySet();
            _passport = value;
            if (Passport == null || Address == null) return;

            IsSuspicious = false;
            foreach (IBankAccount bankAccount in _accounts)
            {
                bankAccount.RemoveSuspiciousLimits();
            }
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (_address != null)
                throw ClientException.AddressAlreadySet();
            _address = value;
            if (Passport == null || Address == null) return;

            IsSuspicious = false;
            foreach (IBankAccount bankAccount in _accounts)
            {
                bankAccount.RemoveSuspiciousLimits();
            }
        }
    }

    public bool IsSuspicious { get; private set; }
    public IReadOnlyList<IBankAccount> Accounts { get; }

    public IBankAccount AddAccount(IBankAccount account)
    {
        _accounts.Add(account);
        return account;
    }

    public void Notify(string message)
    {
        Notification.Notify(message);
    }
}