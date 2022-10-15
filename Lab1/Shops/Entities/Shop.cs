using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private string _name;
    private string _address;
    private int _id;
    private List<Product> _products;

    public Shop(string name, string address, int id)
    {
        _name = name;
        _address = address;
        _id = id;
        _products = new List<Product>();
    }

    public string Name => _name;

    public IReadOnlyList<Product> Products => _products;

    public void AddProducts(params Product[] products)
    {
        foreach (Product newProduct in products)
        {
            Product product = _products.Find(x => x.Name == newProduct.Name);
            if (product != null)
                product.Count += newProduct.Count;
            else
                _products.Add(newProduct);
        }
    }

    public Product GetProduct(string name)
    {
        return _products.Find(product => product.Name == name) ?? throw new ArgumentException(" ");
    }

    public Product FindProduct(string name)
    {
        return _products.Find(product => product.Name == name);
    }

    public void ChangeProductPrice(string name, decimal newPrice)
    {
        Product foundProduct = _products.Find(product => product.Name == name);
        if (foundProduct == null)
            throw new Exception(" ");
        foundProduct.Price = newPrice;
    }

    public void BuyProducts(Person buyer, params ProductToBuy[] products)
    {
        decimal sum = 0;
        foreach (ProductToBuy product in products)
        {
            Product productInShop = GetProduct(product.Name); // TODO: Вынести покупку в отдельный метод
            if (productInShop.Count < product.Count)
                throw new ArgumentException(" ");
            productInShop.Count -= product.Count;
            sum += productInShop.Price * product.Count;
        }

        if (sum < buyer.Money)
            throw new Exception(" ");
        buyer.DeductMoney(sum);
    }
}