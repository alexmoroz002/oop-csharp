namespace Isu.Exceptions;

public class InvalidCourseNumberException : Exception
{
    public InvalidCourseNumberException(string message)
        : base(message) { }
}