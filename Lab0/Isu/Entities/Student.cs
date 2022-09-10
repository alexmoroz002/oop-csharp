namespace Isu.Entities;

public class Student
{
    private int _id;
    private string _name;

    public Student(int id, string name)
    {
        _id = id;
        _name = name;
    }

    public int Id { get; }
}