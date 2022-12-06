using Banks.Interfaces;

namespace Banks.Entities;

public class CreditAccount : IBankAccount, ICommissionChargeable
{
    public IReadOnlyList<ITransaction> Transactions { get; }
    public void ChargeCommission()
    {
        throw new NotImplementedException();
    }
}