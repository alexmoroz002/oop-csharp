using Isu.Entities;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Decorators;

public class IsuDecorator : IIsuService
{
    public IsuDecorator(IIsuService wrapper)
    {
        Wrapper = wrapper;
    }

    protected IIsuService Wrapper { get; set; }
    public Group AddGroup(GroupName groupName, int studentsCount)
    {
        return Wrapper.AddGroup(groupName, studentsCount);
    }

    public Student AddStudent(Group group, string name)
    {
        return Wrapper.AddStudent(group, name);
    }

    public Student GetStudent(int id)
    {
        return Wrapper.GetStudent(id);
    }

    public Student FindStudent(int id)
    {
        return Wrapper.FindStudent(id);
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        return Wrapper.FindStudents(groupName);
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        return Wrapper.FindStudents(courseNumber);
    }

    public Group FindGroup(GroupName groupName)
    {
        return Wrapper.FindGroup(groupName);
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        return Wrapper.FindGroups(courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        Wrapper.ChangeStudentGroup(student, newGroup);
    }
}