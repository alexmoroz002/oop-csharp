namespace Banks.Accounts.Interfaces;

public interface ICommissionChargeable
{
    void ChargeCommission(decimal commission);
}