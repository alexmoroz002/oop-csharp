namespace Banks.Exceptions;

public class ClientException : Exception
{
    private ClientException(string e)
        : base(e) { }

    public static ClientException PassportAlreadySet()
    {
        return new ClientException("Client's passport already set");
    }

    public static ClientException AddressAlreadySet()
    {
        return new ClientException("Client's address already set");
    }
}