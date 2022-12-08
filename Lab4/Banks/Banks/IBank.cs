using Banks.Accounts.Interfaces;
using Banks.Client;

namespace Banks.Banks;

public interface IBank
{
    string Name { get; }
    int InterestRateYear { get; }
    decimal CommissionAmount { get; }
    IReadOnlyList<IClient> Clients { get; }
    IReadOnlyList<IBankAccount> BankAccounts { get; }
    void ChangeInterestRate(int newInterest);
    void ChangeCommissionAmount(decimal newCommission);
    void PerformTransaction(Guid srcAccount, decimal money, Guid destAccount);
    void CancelTransaction(Guid transactionGuid);
}