using Shops.Entities;
using Shops.Exceptions;
using Shops.Manager;
using Shops.Models;
using Xunit;

namespace Shops.Test;

public class ShopTest
{
    private ShopManager _manager = new ();

    [Fact]
    public void AddPersonWithNegativeMoney_ThrowException()
    {
        Assert.Throws<PersonException>(() => new Person(-100));
    }

    [Fact]
    public void AddShopToManager_ShopIsInManager()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        Assert.Contains(shop, _manager.Shops);
    }

    [Fact]
    public void AddProductsToShop_ProductsAreInShop()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        var p1 = new Product("Milk", 100, 5);
        var p2 = new Product("Bread", 50, 10);
        shop.AddProducts(p1, p2);

        Assert.Contains(shop.GetProduct("Milk"), shop.Products);
        Assert.Contains(shop.GetProduct("Bread"), shop.Products);
    }

    [Fact]
    public void ChangeProductPrice_PriceHasChanged()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        shop.AddProducts(new Product("Chocolate", 200, 10));
        Product product = shop.GetProduct("Chocolate");
        decimal oldPrice = product.Price;
        shop.ChangeProductPrice("Chocolate", 199);

        Assert.NotEqual(oldPrice, product.Price);
    }

    [Fact]
    public void AddExistingProduct_CountChanged()
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
    public void BuyProducts_CountChanged()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        var p1 = new Product("Milk", 100, 5);
        var p2 = new Product("Bread", 50, 10);
        var p3 = new Product("Chocolate", 300, 1000);
        shop.AddProducts(p1, p2, p3);
        Person buyer = new Person(1000);
        shop.BuyProducts(buyer, new ProductToBuy("Milk", 5));
        Assert.Equal(0, shop.GetProduct("Milk").Count);
    }

    [Fact]
    public void BuyProducts_NotEnoughProducts_ThrowException()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        var p1 = new Product("Milk", 100, 5);
        var p2 = new Product("Bread", 50, 10);
        var p3 = new Product("Chocolate", 300, 1000);
        shop.AddProducts(p1, p2, p3);
        Person buyer = new Person(1000);
        shop.BuyProducts(buyer, new ProductToBuy("Milk", 5));
        Assert.Throws<ShopException>(() => shop.BuyProducts(buyer, new ProductToBuy("Milk", 1)));
    }

    [Fact]
    public void BuyProducts_NotEnoughMoney_ThrowException()
    {
        Shop shop = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        var p1 = new Product("Milk", 100, 5);
        var p2 = new Product("Bread", 50, 10);
        var p3 = new Product("Chocolate", 300, 1000);
        shop.AddProducts(p1, p2, p3);
        Person buyer = new Person(1000);
        Assert.Throws<ShopException>(() => shop.BuyProducts(buyer, new ProductToBuy("Chocolate", 4)));
    }

    [Theory]
    [InlineData(100, 50, 5, 10)]
    [InlineData(90, 30, 5, 0)]
    [InlineData(80, 50, 1, 10)]
    public void FindCheapestShop_ShopFound(decimal price1, decimal price2, int count1, int count2)
    {
        Shop badShop1 = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        var p1 = new Product("Milk", price1, count1);
        var p2 = new Product("Bread", price2, count2);
        badShop1.AddProducts(p1, p2);

        Shop badShop2 = _manager.AddShop("Devyatochka", "Novosibirsk, Lenin St. 18");
        p1 = new Product("Milk", 120, 10);
        p2 = new Product("Bread", 50, 10);
        badShop2.AddProducts(p1, p2);

        Shop goodShop = _manager.AddShop("Desyatochka", "Novosibirsk, Lenin St. 19");
        p1 = new Product("Milk", 99, 5);
        p2 = new Product("Bread", 50, 10);
        goodShop.AddProducts(p1, p2);

        Assert.Equal(goodShop, _manager.GetCheapestShop(new ProductToBuy("Milk", 2), new ProductToBuy("Bread", 2)));
    }

    [Fact]
    public void FindCheapestShop_ShopNotFound_ThrowException()
    {
        Shop badShop1 = _manager.AddShop("Semerochka", "Novosibirsk, Lenin St. 16");
        var p1 = new Product("Milk", 99, 5);
        var p2 = new Product("Bread", 50, 10);
        badShop1.AddProducts(p1, p2);

        Shop badShop2 = _manager.AddShop("Devyatochka", "Novosibirsk, Lenin St. 18");
        p1 = new Product("Milk", 120, 9);
        p2 = new Product("Bread", 50, 10);
        badShop2.AddProducts(p1, p2);

        Assert.Throws<ShopManagerException>(() => _manager.GetCheapestShop(new ProductToBuy("Milk", 10), new ProductToBuy("Bread", 2)));
    }
}