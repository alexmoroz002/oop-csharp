using Shops.Exceptions;

namespace Shops.Entities;
public class Product
{
    private readonly string _name;
    private decimal _price;
    private int _count;

    public Product(string name, decimal price, int count)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw ProductException.EmptyProductName();
        if (count < 0)
            throw ProductException.NegativeProductCount(name);
        if (price < 0)
            throw ProductException.NegativeProductPrice(name);

        _name = name;
        _price = price;
        _count = count;
    }

    public string Name => _name;

    public decimal Price
    {
        get => _price;
        internal set => _price = value;
    }

    public int Count
    {
        get => _count;
        internal set => _count = value;
    }
}
