using Banks.Banks;
using Banks.Banks.Config;

namespace Banks.CentralBank;

public interface ICentralBank
{
    DateOnly SystemDate { get; }
    IReadOnlyList<IBank> Banks { get; }
    IBank RegisterBank(string name, IConfig config);
    ICentralBank PlusDay();
    ICentralBank PlusMonth();
    ICentralBank PlusYear();
}