using Banks.Accounts.Entities;
using Banks.Accounts.Interfaces;
using Banks.Banks.Config;
using Banks.Client;
using Banks.Exceptions;
using Banks.Transactions;

namespace Banks.Banks;

public class Bank : IBank
{
    private List<IClient> _clients = new ();
    private List<IBankAccount> _accounts = new ();
    private List<IClient> _subscribers = new ();

    public Bank(string name, IConfig config)
    {
        Config = config;
        Name = name;
    }

    public string Name { get; }
    public IConfig Config { get; private set; }
    public IReadOnlyList<IClient> Clients => _clients;
    public IReadOnlyList<IBankAccount> BankAccounts => _accounts;
    public IReadOnlyList<IClient> Subscribers => _subscribers;

    public IClient AddClient(IClient client)
    {
        _clients.Add(client);
        return client;
    }

    public DebitAccount OpenDebitAccount(IClient client)
    {
        var account = new DebitAccount(this, client.IsSuspicious);
        _accounts.Add(account);
        client.AddAccount(account);
        return account;
    }

    public DepositAccount OpenDepositAccount(IClient client, int termMonth, decimal money)
    {
        var account = new DepositAccount(this, termMonth, money, client.IsSuspicious);
        _accounts.Add(account);
        client.AddAccount(account);
        return account;
    }

    public CreditAccount OpenCreditAccount(IClient client)
    {
        var account = new CreditAccount(this, client.IsSuspicious);
        _accounts.Add(account);
        client.AddAccount(account);
        return account;
    }

    public void PutMoney(IBankAccount account, decimal money)
    {
        account.PutMoney(money);
    }

    public void TakeMoney(IBankAccount account, decimal money)
    {
        account.TakeMoney(money);
    }

    public void ChangeAccountTerms(IConfig newConfig)
    {
        Config = newConfig;
        NotifySubscribers("Terms changed");
    }

    public void AccumulateDailyPercents()
    {
        foreach (IBankAccount bankAccount in BankAccounts)
        {
            bankAccount.AccumulateDailyPercent();
        }
    }

    public void AccruePercents()
    {
        foreach (IBankAccount bankAccount in BankAccounts)
        {
            bankAccount.AccruePercents();
        }
    }

    public Guid PerformTransaction(IBankAccount srcAccount, decimal money, IBankAccount destAccount)
    {
        if (srcAccount.Money < money)
        {
            throw BankAccountException.NotEnoughMoney(srcAccount.Money, money);
        }

        srcAccount.TakeMoney(money);
        destAccount.PutMoney(money);
        return srcAccount.AddTransaction(srcAccount, money, destAccount);
    }

    public void CancelTransaction(IClient client, Guid transactionGuid)
    {
        ITransaction transaction = client.Accounts
            .SelectMany(x => x.Transactions)
            .FirstOrDefault(x => x.TransactionId == transactionGuid) ?? throw new ArgumentException();
        transaction.DestAccount.TakeMoney(transaction.Money);
        transaction.SrcAccount.PutMoney(transaction.Money);
        transaction.SrcAccount.RemoveTransaction(transactionGuid);
    }

    public void AddSubscriber(IClient client)
    {
        _subscribers.Add(client);
    }

    public void DeleteSubscriber(IClient client)
    {
        _subscribers.Remove(client);
    }

    public void NotifySubscribers(string message)
    {
        foreach (IClient subscriber in _subscribers)
        {
            subscriber.Notification.Notify(message);
        }
    }
}