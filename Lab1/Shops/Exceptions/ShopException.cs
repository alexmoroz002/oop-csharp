using Shops.Entities;

namespace Shops.Exceptions;

public class ShopException : Exception
{
    private ShopException(string message)
        : base(message) { }

    public static ShopException ProductNotFound(string productName)
    {
        return new ShopException($"Product {productName} not found in shop");
    }

    public static ShopException NotEnoughProducts(Product product)
    {
        return new ShopException($"Not enough products {product.Name} in shop");
    }

    public static ShopException NotEnoughMoney()
    {
        return new ShopException("Not enough money to buy products");
    }
}