using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName("M32071"), 10);
        Student student = service.AddStudent(group, "Ivanov Ivan");
        Assert.Contains(student, service.FindStudents(group.GroupName));
    }

    [Theory]
    [InlineData(10)]
    [InlineData(5)]
    [InlineData(20)]
    public void ReachMaxStudentPerGroup_ThrowException(int groupSize)
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName("A3100"), groupSize);
        for (int i = 0; i < groupSize; i++)
        {
            service.AddStudent(group, "Ivanov Ivan");
        }

        Assert.Throws<GroupOverflowException>(() => service.AddStudent(group, "Sergeev Sergey"));
    }

    [Theory]
    [InlineData("q329c")]
    [InlineData("l9999")]
    [InlineData("a55990")]
    [InlineData("M32081cc")]
    public void CreateGroupWithInvalidName_ThrowException(string groupName)
    {
        var service = new IsuService();
        Assert.Throws<InvalidGroupNameException>(() => service.AddGroup(new GroupName(groupName), 20));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var service = new IsuService();
        Group oldGroup = service.AddGroup(new GroupName("A3100"), 20);
        Group newGroup = service.AddGroup(new GroupName("A3114"), 20);

        Student student = service.AddStudent(oldGroup, "Ivanov Ivan");
        service.ChangeStudentGroup(student, newGroup);

        Assert.Contains(student, newGroup.Students);
        Assert.DoesNotContain(student, oldGroup.Students);
    }
}
