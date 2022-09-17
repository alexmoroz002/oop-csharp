using Isu.Exceptions;

namespace Isu.Entities;

public class StudentsList : List<Student>
{
    public StudentsList(int maxStudentCount)
    {
        MaxStudentCount = maxStudentCount;
    }

    public int MaxStudentCount { get; }

    public new void Add(Student student)
    {
        if (Count >= MaxStudentCount)
            throw new GroupOverflowException("Group already had maximum students");
        base.Add(student);
    }
}
