using Banks.Accounts.Entities;
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
    IReadOnlyList<IClient> Subscribers { get; }
    IClient AddClient(IClient client);
    DebitAccount OpenDebitAccount(IClient client);
    DepositAccount OpenDepositAccount(IClient client, int termMonth, decimal money);
    CreditAccount OpenCreditAccount(IClient client);
    void PutMoney(IBankAccount account, decimal money);
    void TakeMoney(IBankAccount account, decimal money);

    void ChangeAccountTerms(IConfig newConfig);
    void AccumulateDailyPercents();
    void AccruePercents();
    Guid PerformTransaction(IBankAccount srcAccount, decimal money, IBankAccount destAccount);
    void CancelTransaction(IClient client, Guid transactionGuid);
    void AddSubscriber(IClient client);
    void DeleteSubscriber(IClient client);
}