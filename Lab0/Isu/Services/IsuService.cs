using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private List<Group> _groups;
    private int _lastId = 100000;

    public IsuService()
    {
        _groups = new List<Group>();
    }

    public Student FindStudent(int id)
    {
        return _groups.Select(group => group.Students.FirstOrDefault(x => x.Id == id)).FirstOrDefault(foundStudent => foundStudent != null);
    }

    public Student GetStudent(int id)
    {
        Student foundStudent = FindStudent(id);
        if (foundStudent == null)
            throw new StudentNotFoundException(" ");
        return foundStudent;
    }

    public Group FindGroup(GroupName groupName)
    {
        Group foundGroup = _groups.Find(x => x.GroupName.Name == groupName.Name);
        return foundGroup;
    }

    public Group AddGroup(GroupName groupName, int studentsCount)
    {
        Group foundGroup = FindGroup(groupName);
        if (foundGroup != null)
            throw new GroupAlreadyExistsException(" ");
        var newGroup = new Group(groupName, studentsCount);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        var newStudent = new Student(_lastId, name);
        ++_lastId;
        group.AddStudentInGroup(newStudent);
        return newStudent;
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        Group foundGroup = FindGroup(groupName);
        if (foundGroup == null)
            throw new Exception();
        return foundGroup.Students;
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        IReadOnlyList<Group> groupsListReadOnly = _groups.FindAll(x => x.CourseNumber.Year == courseNumber.Year);
        if (groupsListReadOnly == null)
            throw new Exception();
        return groupsListReadOnly;
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        IReadOnlyList<Group> foundGroups = FindGroups(courseNumber);
        return foundGroups.SelectMany(x => x.Students).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        Group oldGroup = _groups.Find(x => x.Students.Contains(student)) ?? throw new InvalidOperationException();

        oldGroup.DeleteStudentFromGroup(student.Id);

        newGroup.AddStudentInGroup(student);
    }
}
