using Banks.Accounts.Interfaces;
using Banks.Notifications;

namespace Banks.Client;

public interface IClient
{
    string Name { get; init; }
    string Surname { get; init; }
    int? Passport { get; set; }
    string Address { get; set; }
    bool IsSuspicious { get; }
    IReadOnlyList<IBankAccount> Accounts { get; }
    INotification Notification { get; }
    IBankAccount AddAccount(IBankAccount account);
}