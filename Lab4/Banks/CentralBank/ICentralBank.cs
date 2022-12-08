using Banks.Banks;

namespace Banks.CentralBank;

public interface ICentralBank
{
    int InterestRate { get; }
    DateOnly SystemDate { get; }
    IReadOnlyList<IBank> Banks { get; }
    IBank RegisterBank(string name);
}