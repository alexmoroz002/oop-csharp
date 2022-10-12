namespace Shops.Entities;

public class Shop
{
    private string _name;
    private string _address;
    private int _id;
    private List<Product> _products;
    private int _lastId = 1;

    public Shop(string name, string address)
    {
        _name = name;
        _address = address;
        _id = _lastId++;
        _products = new List<Product>();
    }

    public string Name { get { return _name; } }

    public void AddProduct(string name, decimal price, int count)
    {
        Product foundProduct = _products.Find(product => product.Name == name);
        if (foundProduct != null)
            foundProduct.Count += count;
        _products.Add(new Product(name, price, count));
    }

    public void ChangeProductPrice(string name, decimal newPrice)
    {
        Product foundProduct = _products.Find(product => product.Name == name);
        if (foundProduct == null)
            throw new Exception(" ");
        foundProduct.Price = newPrice;
    }

    public void BuyProducts(Person buyer, params Product[] products)
    {
        foreach (Product product in products)
        {
            
        }
    }

}