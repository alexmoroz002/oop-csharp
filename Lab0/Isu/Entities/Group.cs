using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private StudentsList _students;
    private GroupName _groupName;

    // public Group(GroupName name, int maxSize = 20)
    public Group(GroupName name, int maxSize)
    {
        _groupName = name;
        _students = new StudentsList(maxSize);
    }

    public Group(StudentsList students, GroupName groupName)
    {
        _students = students;
        _groupName = groupName;
    }

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

    public GroupName GroupName { get { return _groupName; } }
}