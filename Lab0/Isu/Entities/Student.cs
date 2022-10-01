namespace Isu.Entities;

public class Student
{
    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }

    public string Name { get; }
}