using Shops.Exceptions;

namespace Shops.Models;

public class ProductToBuy
{
    private readonly string _name;
    private readonly int _count;

    public ProductToBuy(string name, int count)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw ProductException.EmptyProductName();
        if (count < 0)
            throw ProductException.NegativeProductCount(name);

        _name = name;
        _count = count;
    }

    public string Name => _name;

    public int Count => _count;
}