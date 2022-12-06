using Banks.Interfaces;

namespace Banks.Entities;

public class DebitAccount : IBankAccount
{
    public IReadOnlyList<ITransaction> Transactions { get; }
}