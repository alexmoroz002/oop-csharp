using Isu.Entities;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Decorators;

public class IsuBaseDecorator : IIsuService
{
    protected IsuBaseDecorator(IIsuService wrapper)
    {
        Wrapper = wrapper;
    }

    protected IIsuService Wrapper { get; set; }
    public virtual Group AddGroup(GroupName groupName, int studentsCount)
    {
        return Wrapper.AddGroup(groupName, studentsCount);
    }

    public virtual Student AddStudent(Group group, string name)
    {
        return Wrapper.AddStudent(group, name);
    }

    public virtual Student GetStudent(int id)
    {
        return Wrapper.GetStudent(id);
    }

    public virtual Student FindStudent(int id)
    {
        return Wrapper.FindStudent(id);
    }

    public virtual IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        return Wrapper.FindStudents(groupName);
    }

    public virtual IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        return Wrapper.FindStudents(courseNumber);
    }

    public virtual Group FindGroup(GroupName groupName)
    {
        return Wrapper.FindGroup(groupName);
    }

    public virtual IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        return Wrapper.FindGroups(courseNumber);
    }

    public virtual void ChangeStudentGroup(Student student, Group newGroup)
    {
        Wrapper.ChangeStudentGroup(student, newGroup);
    }
}