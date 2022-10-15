using Shops.Entities;
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

    public Shop FindCheapestShop(params ProductToBuy[] products)
    {
        // var cheapestShopCandidates = new List<Shop>();
        var cheapestShopCandidates = new List<Shop>();
        foreach (var shop in _shops)
        {
            foreach (var productInShop in shop.Products)
            {
                if (products.Any(productToBuy => productToBuy.Name == productInShop.Name && productToBuy.Count <= productInShop.Count))
                {
                    
                    cheapestShopCandidates.Add(shop);
                    break;
                }
            }
        }

        if (!cheapestShopCandidates.Any())
            throw new Exception(" ");
        foreach (var cheapestShopCandidate in cheapestShopCandidates)
        {

        }
        return cheapestShopCandidates.First();
    }
}