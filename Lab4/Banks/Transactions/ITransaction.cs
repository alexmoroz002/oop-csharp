namespace Banks.Transactions;

public interface ITransaction
{
    Guid TransactionId { get; }
}