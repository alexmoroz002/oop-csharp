using Banks.Accounts.Interfaces;
using Banks.Banks.Config;
using Banks.Client;
using Banks.Transactions;

namespace Banks.Banks;

public class Bank : IBank
{
    private List<IClient> _clients = new ();
    private List<IBankAccount> _accounts = new ();

    public Bank(string name, IConfig config)
    {
        Config = config;
        Name = name;
    }

    public string Name { get; }
    public IConfig Config { get; private set; }
    public IReadOnlyList<IClient> Clients => _clients;
    public IReadOnlyList<IBankAccount> BankAccounts => _accounts;

    public void ChangeAccountTerms(IConfig newConfig)
    {
        Config = newConfig;
    }

    public void AccruePercents()
    {
        foreach (IBankAccount bankAccount in BankAccounts)
        {
            bankAccount.AccrueDailyPercent();
        }
    }

    public Guid PerformTransaction(IBankAccount srcAccount, decimal money, IBankAccount destAccount)
    {
        if (srcAccount.Money < money)
        {
            throw new ArgumentException();
        }

        srcAccount.TakeMoney(money); // (decimal money, bool override = false) ?
        destAccount.PutMoney(money); // -//-
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
}