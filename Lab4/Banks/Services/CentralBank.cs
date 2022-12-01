namespace Banks.Services;

public class CentralBank
{
    private static CentralBank _bank;
    private int x;

    private CentralBank(int x)
    {
        this.x = x;
    }

    public static CentralBank GetInstance(int x)
    {
        if (_bank == null)
        {
            _bank = new CentralBank(x);
        }

        return _bank;
    }
}