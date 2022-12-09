using Banks.Banks;
using Banks.Transactions;

namespace Banks.Accounts.Interfaces;

public interface IBankAccount
{
    bool IsSuspicious { get; }
    IReadOnlyList<ITransaction> Transactions { get; }
    decimal Money { get; }
    public IBank Bank { get; }
    void AccrueDailyPercent();
    void RemoveSuspiciousLimits();
    Guid AddTransaction(IBankAccount srcAccount, decimal money, IBankAccount destAccount);
    void RemoveTransaction(Guid transactionGuid);
    void PutMoney(decimal amount);
    void TakeMoney(decimal amount);
}