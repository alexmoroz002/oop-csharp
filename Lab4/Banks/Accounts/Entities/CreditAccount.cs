using Banks.Accounts.Interfaces;
using Banks.Transactions;

namespace Banks.Accounts.Entities;

public class CreditAccount : IBankAccount, ICommissionChargeable
{
    public bool IsSuspicious { get; }
    public IReadOnlyList<ITransaction> Transactions { get; }
    public decimal Money { get; private set; }
    public decimal SuspiciousAccountTransactionLimit { get; }

    public void UpdateTerms()
    {
        throw new NotImplementedException();
    }

    public decimal CreditLimit { get; }

    public void ChargeCommission(decimal commission)
    {
        throw new NotImplementedException();
    }
}