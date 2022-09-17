using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private StudentsList _students;

    public Group(GroupName name, int maxSize)
    {
        GroupName = name;
        _students = new StudentsList(maxSize);
        CourseNumber = new CourseNumber(Convert.ToInt32(GroupName.Name[2]));
    }

    public GroupName GroupName { get; }

    public CourseNumber CourseNumber { get; }

    public int StudentsLimit => _students.MaxStudentCount;

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
        _students.Add(student);
    }

    public void DeleteStudentFromGroup(int id)
    {
        if (_students.RemoveAll((x) => x.Id == id) == 0)
            throw new StudentNotFoundException(" ");
    }
}