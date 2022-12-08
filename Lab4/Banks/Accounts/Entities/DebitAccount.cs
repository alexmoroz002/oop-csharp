using Banks.Accounts.Interfaces;
using Banks.Transactions;

namespace Banks.Accounts.Entities;

public class DebitAccount : IBankAccount, IPercentAccruable
{
    public IReadOnlyList<ITransaction> Transactions { get; }
    public decimal Money { get; private set; }
    public void UpdateTerms()
    {
        throw new NotImplementedException();
    }

    public void AccrueDailyPercent(int dailyPercent)
    {
        throw new NotImplementedException();
    }
}