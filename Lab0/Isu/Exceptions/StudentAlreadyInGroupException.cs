namespace Isu.Exceptions;
public class StudentAlreadyInGroupException : Exception
{
    public StudentAlreadyInGroupException(string messsage)
        : base(messsage) { }
}
