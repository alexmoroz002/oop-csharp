using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private string _name;
    private string _address;
    private int _id;
    private List<Product> _products;

    internal Shop(string name, string address, int id)
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
            if (newProduct.Count == 0)
                continue;
            Product product = FindProduct(newProduct.Name);
            if (product != null)
                product.Count += newProduct.Count;
            else
                _products.Add(newProduct);
        }
    }

    public Product GetProduct(string name)
    {
        return FindProduct(name) ?? throw ShopException.ProductNotFound(name);
    }

    public Product FindProduct(string name)
    {
        return _products.Find(product => product.Name == name);
    }

    public void ChangeProductPrice(string name, decimal newPrice)
    {
        Product foundProduct = GetProduct(name);
        foundProduct.Price = newPrice;
    }

    public void BuyProducts(Person buyer, params ProductToBuy[] products)
    {
        if (products.Length == 0)
            return;
        decimal sum = 0;
        foreach (ProductToBuy product in products)
        {
            Product productInShop = GetProduct(product.Name);
            if (productInShop.Count < product.Count)
                throw ShopException.NotEnoughProducts(productInShop);
            productInShop.Count -= product.Count;
            sum += productInShop.Price * product.Count;
        }

        if (sum > buyer.Money)
            throw ShopException.NotEnoughMoney();
        buyer.DeductMoney(sum);
    }
}