namespace Shops.Exceptions;

public class PersonException : Exception
{
    private PersonException(string message)
        : base(message) { }

    public static PersonException NegativeMoneyAmount()
    {
        return new PersonException("Money amount can't be negative");
    }
}