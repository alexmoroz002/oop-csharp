using Banks.Accounts.Interfaces;
using Banks.Banks;
using Banks.Exceptions;
using Banks.Transactions;
using Transaction = Banks.Transactions.Transaction;

namespace Banks.Accounts.Entities;

public class DepositAccount : IBankAccount
{
    private List<Transaction> _transactions;
    private decimal _accumulatedMoney;
    public DepositAccount(IBank bank, int? accountTermMonth, decimal money, bool isSuspicious)
    {
        Bank = bank;
        AccountTermMonth = accountTermMonth;
        IsSuspicious = isSuspicious;
        _transactions = new List<Transaction>();
        ActiveMonth = 0;
        Money = money;
        _accumulatedMoney = 0;
        InterestRate = Money switch
        {
            < 5000 => Bank.Config.DepositInterestFirst,
            < 10000 => Bank.Config.DepositInterestSecond,
            _ => Bank.Config.DepositInterestThird
        };
    }

    public decimal InterestRate { get; }
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
            throw BankAccountException.TransactionDoesNotExist(transactionGuid);
    }

    public void PutMoney(decimal amount)
    {
        Money += amount;
    }

    public void TakeMoney(decimal amount)
    {
        if (ActiveMonth < AccountTermMonth)
            throw BankAccountException.DepositNotExpired();
        if (IsSuspicious && amount > Bank.Config.SuspiciousLimit)
            throw BankAccountException.SuspiciousLimitReached();
        Money -= amount;
    }

    public void AccumulateDailyPercent()
    {
        _accumulatedMoney += Money * (InterestRate / 100 / 365);
    }

    public void AccruePercents()
    {
        Money += _accumulatedMoney;
        _accumulatedMoney = 0;
        ActiveMonth++;
    }
}