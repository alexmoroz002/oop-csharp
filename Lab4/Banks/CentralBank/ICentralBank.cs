using Banks.Banks;

namespace Banks.CentralBank;

public interface ICentralBank
{
    IBank RegisterBank();
}