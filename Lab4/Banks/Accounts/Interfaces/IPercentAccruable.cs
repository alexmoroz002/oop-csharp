namespace Banks.Accounts.Interfaces;

public interface IPercentAccruable
{
    DateOnly EndDate { get; }
    void AccrueDailyPercent(int dailyPercent);
}