using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private StudentsList _students;
    private GroupName _groupName;

    public Group(GroupName name)
    {
        _groupName = name;
        _students = new StudentsList();
    }

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

    public StudentsList Students { get { return _students; } }
}