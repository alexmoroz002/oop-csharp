using Banks.Accounts.Interfaces;
using Banks.Client;

namespace Banks.Banks;

public class Bank : IBank
{
    private List<IClient> _clients;
    private List<IBankAccount> _accounts;
    public string Name { get; }
    public int InterestRateYear { get; }
    public decimal CommissionAmount { get; }
    public IReadOnlyList<IClient> Clients => _clients;
    public IReadOnlyList<IBankAccount> BankAccounts => _accounts;
    public void ChangeInterestRate()
    {
        throw new NotImplementedException();
    }

    public void ChangeCommissionAmount()
    {
        throw new NotImplementedException();
    }

    public void ChargeCommissions()
    {
        throw new NotImplementedException();
    }

    public void AccruePercents()
    {
        throw new NotImplementedException();
    }

    public void PerformTransaction()
    {
        throw new NotImplementedException();
    }

    public void CancelTransaction()
    {
        throw new NotImplementedException();
    }
}