namespace Shops.Entities;
public class Product // Maybe its better to inherit Product from ProductToBuy? Examine
{
    private readonly string _name;
    private decimal _price;
    private int _count;

    public Product(string name, decimal price, int count = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("name");
        if (count < 0)
            throw new ArgumentOutOfRangeException("count");
        if (price < 0)
            throw new InvalidOperationException(" ");

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
