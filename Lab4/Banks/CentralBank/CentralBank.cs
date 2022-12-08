using Banks.Banks;

namespace Banks.CentralBank;

public class CentralBank : ICentralBank
{
    private List<IBank> _banks;

    public CentralBank()
    {
        SystemDate = DateOnly.FromDateTime(DateTime.Now);
        _banks = new List<IBank>();
        InterestRate = 10;
    }

    public int InterestRate { get; }
    public DateOnly SystemDate { get; }

    public IReadOnlyList<IBank> Banks => _banks;

    public IBank RegisterBank(string name)
    {
        var bank = new Bank(name, InterestRate);
        _banks.Add(bank);
        return bank;
    }
}