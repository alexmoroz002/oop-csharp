namespace Banks.Banks.Config;

public class Config : IConfig
{
    public Config(int debitInterest, int depositInterestFirst, int depositInterestSecond, int depositInterestThird, decimal creditCommission, decimal suspiciousLimit, decimal creditLimit)
    {
        DebitInterest = debitInterest;
        DepositInterestFirst = depositInterestFirst;
        DepositInterestSecond = depositInterestSecond;
        DepositInterestThird = depositInterestThird;
        CreditCommission = creditCommission;
        SuspiciousLimit = suspiciousLimit;
        CreditAccountLimit = creditLimit;
    }

    public int DebitInterest { get; }
    public int DepositInterestFirst { get; }
    public int DepositInterestSecond { get; }
    public int DepositInterestThird { get; }
    public decimal CreditCommission { get; }
    public decimal SuspiciousLimit { get; }
    public decimal CreditAccountLimit { get; }
}