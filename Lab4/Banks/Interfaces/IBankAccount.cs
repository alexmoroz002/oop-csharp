namespace Banks.Interfaces;

public interface IBankAccount
{
    IReadOnlyList<ITransaction> Transactions { get; }
}