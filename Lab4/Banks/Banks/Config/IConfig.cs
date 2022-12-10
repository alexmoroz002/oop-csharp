namespace Banks.Banks.Config;

public interface IConfig
{
    decimal DebitInterest { get; }
    decimal DepositInterestFirst { get; }
    decimal DepositInterestSecond { get; }
    decimal DepositInterestThird { get; }
    decimal CreditCommission { get; }
    decimal SuspiciousLimit { get; }
    decimal CreditAccountLimit { get; }
}