namespace Banks.Banks.Config;

public class Config : IConfig
{
    public Config(decimal debitInterest, decimal depositInterestFirst, decimal depositInterestSecond, decimal depositInterestThird, decimal creditCommission, decimal suspiciousLimit, decimal creditLimit)
    {
        DebitInterest = debitInterest;
        DepositInterestFirst = depositInterestFirst;
        DepositInterestSecond = depositInterestSecond;
        DepositInterestThird = depositInterestThird;
        CreditCommission = creditCommission;
        SuspiciousLimit = suspiciousLimit;
        CreditAccountLimit = creditLimit;
    }

    public decimal DebitInterest { get; }
    public decimal DepositInterestFirst { get; }
    public decimal DepositInterestSecond { get; }
    public decimal DepositInterestThird { get; }
    public decimal CreditCommission { get; }
    public decimal SuspiciousLimit { get; }
    public decimal CreditAccountLimit { get; }
}