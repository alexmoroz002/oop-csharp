using Banks.Accounts.Interfaces;
using Banks.Transactions;

namespace Banks.Accounts.Entities;

public class CreditAccount : IBankAccount, ICommissionChargeable
{
    public IReadOnlyList<ITransaction> Transactions { get; }
    public decimal Money { get; private set; }
    public void UpdateTerms()
    {
        throw new NotImplementedException();
    }

    public void ChargeCommission(decimal commission)
    {
        throw new NotImplementedException();
    }
}