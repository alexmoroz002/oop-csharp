using Banks.Accounts.Interfaces;
using Banks.Banks;
using Banks.Transactions;
using Transaction = Banks.Transactions.Transaction;

namespace Banks.Accounts.Entities;

public class DepositAccount : IBankAccount
{
    private List<Transaction> _transactions;
    public DepositAccount(IBank bank, int? accountTermMonth, bool isSuspicious)
    {
        Bank = bank;
        AccountTermMonth = accountTermMonth;
        IsSuspicious = isSuspicious;
        _transactions = new List<Transaction>();
        ActiveMonth = 0;
    }

    public bool IsSuspicious { get; private set; }
    public IReadOnlyList<ITransaction> Transactions => _transactions;
    public decimal Money { get; private set; }
    public IBank Bank { get; }
    public int? AccountTermMonth { get; }
    private int ActiveMonth { get; set; }

    public void RemoveSuspiciousLimits()
    {
        IsSuspicious = false;
    }

    public Guid AddTransaction(IBankAccount srcAccount, decimal money, IBankAccount destAccount)
    {
        var transaction = new Transaction(srcAccount, money, destAccount);
        _transactions.Add(transaction);
        return transaction.TransactionId;
    }

    public void RemoveTransaction(Guid transactionGuid)
    {
        int removed = _transactions.RemoveAll(x => x.TransactionId == transactionGuid);
        if (removed == 0)
            throw new ArgumentException();
    }

    public void PutMoney(decimal amount)
    {
        Money += amount;
    }

    public void TakeMoney(decimal amount)
    {
        if (ActiveMonth < AccountTermMonth)
            throw new ArgumentException();
        Money -= amount;
    }

    public void AccrueDailyPercent()
    {
        throw new NotImplementedException();
    }
}