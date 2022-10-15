using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Manager;
public class ShopManager
{
    private List<Shop> _shops;
    private int _shopId = 1;

    public ShopManager()
    {
        _shops = new List<Shop>();
    }

    public IReadOnlyList<Shop> Shops => _shops;

    public Shop AddShop(string name, string address)
    {
        var newShop = new Shop(name, address, _shopId++);
        _shops.Add(newShop);
        return newShop;
    }

    public Shop GetCheapestShop(params ProductToBuy[] products)
    {
        Shop cheapestShop = null;
        decimal minSum = decimal.MaxValue;
        foreach (Shop shop in _shops)
        {
            decimal sum = 0;
            bool enoughProducts = true;
            foreach (ProductToBuy productToBuy in products)
            {
                if (productToBuy == null)
                    throw ProductException.NullProduct();
                Product productInShop = shop.FindProduct(productToBuy.Name);
                if (productInShop == null || productInShop.Count < productToBuy.Count)
                {
                    enoughProducts = false;
                    break;
                }

                sum += productInShop.Price * productToBuy.Count;
            }

            if (enoughProducts && sum <= minSum)
            {
                minSum = sum;
                cheapestShop = shop;
            }
        }

        return cheapestShop ?? throw ShopManagerException.CheapestShopNotFound();
    }
}