using Banks.Banks.Config;

namespace Banks.Banks;

public class CentralBank : ICentralBank
{
    private const int MonthCount = 12;
    private List<IBank> _banks;
    private int _daysPassed;
    private int _daysInMonth;

    public CentralBank()
    {
        SystemDate = DateOnly.FromDateTime(DateTime.Now);
        _banks = new List<IBank>();
        _daysPassed = 0;
        _daysInMonth = DateTime.DaysInMonth(SystemDate.Year, SystemDate.Month);
    }

    public DateOnly SystemDate { get; }
    public IReadOnlyList<IBank> Banks => _banks;

    public IBank RegisterBank(string name, IConfig config)
    {
        var bank = new Bank(name, config);
        _banks.Add(bank);
        return bank;
    }

    public ICentralBank PlusDay()
    {
        _daysPassed++;
        if (_daysPassed == _daysInMonth)
        {
            _daysInMonth = DateTime.DaysInMonth(SystemDate.Year, SystemDate.Month);
            _daysPassed = 0;
            _banks.ForEach(x =>
            {
                x.AccumulateDailyPercents();
                x.AccruePercents();
            });
            return this;
        }

        _banks.ForEach(x => x.AccumulateDailyPercents());
        return this;
    }

    public ICentralBank PlusMonth()
    {
        for (int i = 0; i < _daysInMonth; i++)
        {
            PlusDay();
        }

        return this;
    }

    public ICentralBank PlusYear()
    {
        for (int i = 0; i < MonthCount; i++)
        {
            PlusMonth();
        }

        return this;
    }
}