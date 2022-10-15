namespace Shops.Exceptions;

public class ShopManagerException : Exception
{
    private ShopManagerException(string message)
        : base(message) { }

    public static ShopManagerException CheapestShopNotFound()
    {
        return new ShopManagerException("Shop with lowest price for product pack not found");
    }
}