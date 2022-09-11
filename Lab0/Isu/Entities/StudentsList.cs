namespace Isu.Entities;
public class StudentsList : List<Student>
{
    private readonly int _maxStudentCount;

    public StudentsList(int maxStudentCount)
        : base()
    {
        _maxStudentCount = maxStudentCount;
    }

    public int MaxStudentCount { get { return _maxStudentCount; } }

    public new void Add(Student student)
    {
        if (Count >= _maxStudentCount)
            throw new ArgumentException(); // TODO: Change ex type
        base.Add(student);
    }
}
