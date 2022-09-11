using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

internal class IsuService : IIsuService
{
    private List<Group> _groups;
    private int _lastId = 100000;

    public IsuService()
    {
        _groups = new List<Group>();
    }

    public Student? FindStudent(int id)
    {
        foreach (Group group in _groups)
        {
            Student? foundStudent = group.Students.FirstOrDefault(x => x.Id == id);
            if (foundStudent != null)
                return foundStudent;
        }

        return null;
    }

    public Student GetStudent(int id)
    {
        Student? foundStudent = FindStudent(id);
        if (foundStudent == null)
            throw new ArgumentException();
        return foundStudent;
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(x => x.GroupName == groupName);
    }

    public Group AddGroup(GroupName name, int studentsCount = 20)
    {
        if (FindGroup(name) != null)
            throw new ArgumentException(string.Format("s"));
        var newGroup = new Group(name, studentsCount);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        var newStudent = new Student(_lastId, name);

        // TODO: event - added student
        _lastId++;
        group.AddStudentInGroup(newStudent);
        return newStudent;
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        Group? foundGroup = _groups.Find(x => x.GroupName == groupName);
        if (foundGroup == null)
            throw new Exception();
        return foundGroup.Students;
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        throw new NotImplementedException();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        throw new NotImplementedException();
    }
}
