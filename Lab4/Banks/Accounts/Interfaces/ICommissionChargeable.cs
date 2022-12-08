namespace Banks.Accounts.Interfaces;

public interface ICommissionChargeable
{
    decimal CreditLimit { get; }
    void ChargeCommission(decimal commission);
}