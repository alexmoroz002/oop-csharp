using Banks.Accounts.Interfaces;
using Banks.Banks;
using Banks.Exceptions;
using Banks.Transactions;

namespace Banks.Accounts.Entities;

public class CreditAccount : IBankAccount
{
    private List<Transaction> _transactions;

    public CreditAccount(IBank bank, bool isSuspicious)
    {
        Bank = bank;
        IsSuspicious = isSuspicious;
        _transactions = new List<Transaction>();
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
            throw BankAccountException.TransactionDoesNotExist(transactionGuid);
    }

    public void PutMoney(decimal amount)
    {
        Money += amount;
    }

    public void TakeMoney(decimal amount)
    {
        if (Money - amount < Bank.Config.CreditAccountLimit)
            throw BankAccountException.CreditLimitReached();
        if (IsSuspicious && amount > Bank.Config.SuspiciousLimit)
            throw BankAccountException.SuspiciousLimitReached();
        Money -= amount;
    }

    public void AccumulateDailyPercent()
    {
    }

    public void AccruePercents()
    {
    }
}