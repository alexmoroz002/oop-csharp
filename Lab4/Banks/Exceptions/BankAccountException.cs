namespace Banks.Exceptions;

public class BankAccountException : Exception
{
    private BankAccountException(string e)
        : base(e) { }

    public static BankAccountException TransactionDoesNotExist(Guid id)
    {
        return new BankAccountException($"Transaction with guid {id} does not exist");
    }

    public static BankAccountException CreditLimitReached()
    {
        return new BankAccountException("Credit limit reached");
    }

    public static BankAccountException SuspiciousLimitReached()
    {
        return new BankAccountException("Transaction limit for suspicious accounts reached");
    }

    public static BankAccountException DepositNotExpired()
    {
        return new BankAccountException("Trying to take money from active deposit account");
    }

    public static BankAccountException NotEnoughMoney(decimal from, decimal sum)
    {
        return new BankAccountException($"Account has {from} money, trying to take {sum}");
    }
}