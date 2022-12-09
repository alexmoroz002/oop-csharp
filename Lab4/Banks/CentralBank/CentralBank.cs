using Banks.Banks;
using Banks.Banks.Config;

namespace Banks.CentralBank;

public class CentralBank : ICentralBank
{
    private List<IBank> _banks;

    public CentralBank()
    {
        SystemDate = DateOnly.FromDateTime(DateTime.Now);
        _banks = new List<IBank>();
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
        throw new NotImplementedException();
    }

    public ICentralBank PlusMonth()
    {
        throw new NotImplementedException();
    }

    public ICentralBank PlusYear()
    {
        throw new NotImplementedException();
    }
}