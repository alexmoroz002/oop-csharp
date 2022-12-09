using Banks.Accounts.Interfaces;

namespace Banks.Transactions;

public class Transaction : ITransaction
{
    public Transaction(IBankAccount srcAccount, decimal money, IBankAccount destAccount)
    {
        Money = money;
        SrcAccount = srcAccount;
        DestAccount = destAccount;
        TransactionId = Guid.NewGuid();
    }

    public Guid TransactionId { get; }
    public decimal Money { get; }
    public IBankAccount SrcAccount { get; }
    public IBankAccount DestAccount { get; }
}