namespace Shops.Entities;
public class Person
{
    private decimal _money;

    public Person(decimal money)
    {
        _money = money;
    }

    public decimal Money => _money;
}
