namespace Banks.Banks.Config;

public interface IConfig
{
    int DebitInterest { get; }
    int DepositInterestFirst { get; }
    int DepositInterestSecond { get; }
    int DepositInterestThird { get; }
    decimal CreditCommission { get; }
    decimal SuspiciousLimit { get; }
    decimal CreditAccountLimit { get; }
}