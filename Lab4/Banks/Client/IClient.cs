using Banks.Accounts.Interfaces;

namespace Banks.Client;

public interface IClient
{
    string Name { get; init; }
    string Surname { get; init; }
    public int? Passport { get; set; }
    public string Address { get; set; }
    bool IsSuspicious { get; }
    IReadOnlyList<IBankAccount> Accounts { get; }
    IBankAccount AddAccount(IBankAccount account);
    void Notify(string message);
}