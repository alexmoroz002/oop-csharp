namespace Isu.Exceptions;

public class GroupOverflowException : OverflowException
{
    public GroupOverflowException(string message)
        : base(message) { }
}
