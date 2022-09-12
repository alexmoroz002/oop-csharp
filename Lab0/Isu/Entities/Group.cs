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
        _courseNumber = CourseYearNumber;
    }

    public Group(StudentsList students, GroupName groupName)
    {
        _students = students;
        _groupName = groupName;
        _courseNumber = CourseYearNumber;
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

    public CourseNumber CourseYearNumber
    {
        get
        {
            if (char.IsDigit(GroupName.Name.First()))
                return new CourseNumber(Convert.ToInt32(GroupName.Name.First()));
            else if (Convert.ToInt32(GroupName.Name[1]) != 4)
                return new CourseNumber(Convert.ToInt32(GroupName.Name[2]));
            else if (Convert.ToInt32(GroupName.Name[1]) == 4)
                return new CourseNumber(Convert.ToInt32(GroupName.Name[2]) + 4);
            else
                throw new Exception();
        }
    }
}