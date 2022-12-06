using Banks.Interfaces;

namespace Banks.Entities;

public class DepositAccount : IBankAccount
{
    public IReadOnlyList<ITransaction> Transactions { get; }
}