using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private StudentsList _students;
    private GroupName _groupName;
    private CourseNumber _courseNumber;

    public Group(GroupName name, int maxSize)
    {
        _groupName = name;
        _students = new StudentsList(maxSize);
        _courseNumber = CourseNumber;
    }

    public Group(StudentsList students, GroupName groupName)
    {
        _students = students;
        _groupName = groupName;
        _courseNumber = CourseNumber;
    }

    public GroupName GroupName => _groupName;

    public CourseNumber CourseNumber => new CourseNumber(Convert.ToInt32(GroupName.Name[2]));

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
}