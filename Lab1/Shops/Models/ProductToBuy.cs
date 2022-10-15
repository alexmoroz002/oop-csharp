namespace Shops.Models;

public class ProductToBuy
{
    private readonly string _name;
    private int _count;

    public ProductToBuy(string name, int count = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("name");
        if (count < 0)
            throw new ArgumentOutOfRangeException("count");

        _name = name;
        _count = count;
    }

    public string Name => _name;

    public int Count => _count;
}