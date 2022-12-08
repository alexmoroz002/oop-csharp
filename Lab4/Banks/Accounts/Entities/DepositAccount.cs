using Banks.Accounts.Interfaces;
using Banks.Transactions;

namespace Banks.Accounts.Entities;

public class DepositAccount : IBankAccount, IPercentAccruable
{
    public bool IsSuspicious { get; private set; }
    public IReadOnlyList<ITransaction> Transactions { get; }
    public decimal Money { get; private set; }
    public decimal SuspiciousAccountTransactionLimit { get; }

    public void UpdateTerms()
    {
        IsSuspicious = false;
    }

    public DateOnly EndDate { get; }

    public void AccrueDailyPercent(int dailyPercent)
    {
        throw new NotImplementedException();
    }
}