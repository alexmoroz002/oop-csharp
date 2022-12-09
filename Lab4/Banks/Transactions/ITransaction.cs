using Banks.Accounts.Interfaces;

namespace Banks.Transactions;

public interface ITransaction
{
    Guid TransactionId { get; }
    decimal Money { get; }
    IBankAccount SrcAccount { get; }
    IBankAccount DestAccount { get; }
}