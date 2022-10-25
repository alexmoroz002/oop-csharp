namespace Isu.Extra.Entities;

public class Teacher
{
    public Teacher(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }
}