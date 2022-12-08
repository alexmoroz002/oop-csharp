namespace Banks.Accounts.Interfaces;

public interface IPercentAccruable
{
    void AccrueDailyPercent(int dailyPercent);
}