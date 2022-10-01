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
        return _groups.SelectMany(group => group.Students).FirstOrDefault(x => x.Id == id);
    }

    public Student GetStudent(int id)
    {
        Student foundStudent = FindStudent(id);
        if (foundStudent == null)
            throw new StudentNotFoundException("Student was not found");
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
            throw new GroupAlreadyExistsException("Group with this name already exists");
        var newGroup = new Group(groupName, studentsCount);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        var newStudent = new Student(_lastId++, name);
        group.AddStudentInGroup(newStudent);
        return newStudent;
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        Group foundGroup = FindGroup(groupName);
        if (foundGroup == null)
            return Enumerable.Empty<Student>().ToList().AsReadOnly();
        return foundGroup.Students;
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.FindAll(x => x.CourseNumber.Year == courseNumber.Year);
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
