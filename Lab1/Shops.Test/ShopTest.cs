using Shops.Entities;
using Shops.Manager;
using Xunit;

namespace Shops.Test;

public class ShopTest
{
    private ShopManager _manager = new ShopManager();

    [Fact]
    public void AddShopToManager()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        Assert.Contains(shop, _manager.Shops);
    }

    [Fact]
    public void AddProductsToShopAndGet()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        var p1 = new Product("Milk", 100, 5);
        var p2 = new Product("Bread", 50, 10);
        shop.AddProducts(p1, p2);

        Assert.Contains(shop.GetProduct("Milk"), shop.Products);
        Assert.Contains(shop.GetProduct("Bread"), shop.Products);
    }

    [Fact]
    public void ChangeProductPrice()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        shop.AddProducts(new Product("Chocolate", 200, 10));
        Product product = shop.GetProduct("Chocolate");
        decimal oldPrice = product.Price;
        shop.ChangeProductPrice("Chocolate", 199);

        Assert.NotEqual(oldPrice, product.Price);
    }

    [Fact]
    public void AddExistingProduct()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        decimal supplyPrice = 200,
            newSupplyPrice = 100;
        int supplyCount = 10,
            newSupplyCount = 20;

        shop.AddProducts(new Product("Chocolate", supplyPrice, supplyCount));
        shop.AddProducts(new Product("Chocolate", newSupplyPrice, newSupplyCount));
        Product product = shop.GetProduct("Chocolate");

        Assert.Single(shop.Products);
        Assert.NotEqual(product.Price, newSupplyPrice);
        Assert.Equal(product.Count, supplyCount + newSupplyCount);
    }

    [Fact]
    public void BuyProducts()
    {
        // Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
    }
}