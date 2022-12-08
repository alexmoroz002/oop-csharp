using Banks.Accounts.Interfaces;
using Banks.Client;

namespace Banks.Banks;

public class Bank : IBank
{
    private const decimal DefaultCommission = 5;
    private List<IClient> _clients = new ();
    private List<IBankAccount> _accounts = new ();

    public Bank(string name, int interestRateYear)
    {
        Name = name;
        InterestRateYear = interestRateYear;
        CommissionAmount = DefaultCommission;
    }

    public string Name { get; }
    public int InterestRateYear { get; private set; }
    public decimal CommissionAmount { get; private set; }
    public IReadOnlyList<IClient> Clients => _clients;
    public IReadOnlyList<IBankAccount> BankAccounts => _accounts;

    public void ChangeInterestRate(int newInterest)
    {
        throw new NotImplementedException();
    }

    public void ChangeCommissionAmount(decimal newCommission)
    {
        CommissionAmount = newCommission;
    }

    public void ChargeCommissions()
    {
        throw new NotImplementedException();
    }

    public void AccruePercents()
    {
        throw new NotImplementedException();
    }

    public void PerformTransaction(Guid srcAccount, decimal money, Guid destAccount)
    {
        throw new NotImplementedException();
    }

    public void CancelTransaction(Guid transactionGuid)
    {
        throw new NotImplementedException();
    }
}