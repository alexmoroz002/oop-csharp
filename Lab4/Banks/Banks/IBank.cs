using Banks.Accounts.Interfaces;
using Banks.Banks.Config;
using Banks.Client;

namespace Banks.Banks;

public interface IBank
{
    string Name { get; }
    IConfig Config { get; }
    IReadOnlyList<IClient> Clients { get; }
    IReadOnlyList<IBankAccount> BankAccounts { get; }
    void ChangeAccountTerms(IConfig newConfig);
    void AccruePercents();
    Guid PerformTransaction(IBankAccount srcAccount, decimal money, IBankAccount destAccount);
    void CancelTransaction(IClient client, Guid transactionGuid);
}