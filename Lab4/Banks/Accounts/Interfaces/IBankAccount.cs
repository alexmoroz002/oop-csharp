using Banks.Transactions;

namespace Banks.Accounts.Interfaces;

public interface IBankAccount
{
    bool IsSuspicious { get; }
    IReadOnlyList<ITransaction> Transactions { get; }
    decimal Money { get; }
    decimal SuspiciousAccountTransactionLimit { get; }
    void UpdateTerms();
}