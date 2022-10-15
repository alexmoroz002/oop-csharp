namespace Shops.Exceptions;

public class ProductException : Exception
{
    private ProductException(string message)
        : base(message) { }

    public static ProductException EmptyProductName()
    {
        return new ProductException("Product name is empty");
    }

    public static ProductException NegativeProductCount(string productName)
    {
        return new ProductException($"Product {productName} count is negative");
    }

    public static ProductException NegativeProductPrice(string productName)
    {
        return new ProductException($"Product {productName} price is negative");
    }
}