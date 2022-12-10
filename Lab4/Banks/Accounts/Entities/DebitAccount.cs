using Banks.Accounts.Interfaces;
using Banks.Banks;
using Banks.Transactions;

namespace Banks.Accounts.Entities;

public class DebitAccount : IBankAccount
{
    private List<Transaction> _transactions;
    private decimal _accumulatedMoney;

    public DebitAccount(IBank bank, bool isSuspicious)
    {
        IsSuspicious = isSuspicious;
        _transactions = new List<Transaction>();
        Bank = bank;
        _accumulatedMoney = 0;
    }

    public bool IsSuspicious { get; private set; }
    public IReadOnlyList<ITransaction> Transactions => _transactions;
    public decimal Money { get; private set; }
    public IBank Bank { get; }

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
        Money -= amount;
        if (IsSuspicious && amount > Bank.Config.SuspiciousLimit)
            throw new ArgumentException();
    }

    public void AccumulateDailyPercent()
    {
        _accumulatedMoney += Money * (Bank.Config.DebitInterest / 100 / 365);
    }

    public void AccruePercents()
    {
        Money += _accumulatedMoney;
        _accumulatedMoney = 0;
    }
}