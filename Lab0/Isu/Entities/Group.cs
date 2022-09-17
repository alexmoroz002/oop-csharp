using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private const int StudentsLimit = 20;
    private StudentsList _students;

    public Group(GroupName name, int maxSize = StudentsLimit)
    {
        GroupName = name;
        _students = new StudentsList(maxSize);
        CourseNumber = new CourseNumber((int)char.GetNumericValue(GroupName.Name[2]));
    }

    public GroupName GroupName { get; }

    public CourseNumber CourseNumber { get; }

    public IReadOnlyList<Student> Students
    {
        get
        {
            IReadOnlyList<Student> studentsListReadOnly = _students;
            return studentsListReadOnly;
        }
    }

    public void AddStudentInGroup(Student student)
    {
        if (_students.Exists(x => x == student))
            throw new StudentAlreadyInGroupException(" ");
        _students.Add(student);
    }

    public void DeleteStudentFromGroup(int id)
    {
        if (_students.RemoveAll((x) => x.Id == id) == 0)
            throw new StudentNotFoundException(" ");
    }
}