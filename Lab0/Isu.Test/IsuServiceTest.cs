using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private IsuService _service = new IsuService();

    [Fact]
    public void AddStudentToIsu_StudentIsInIsu()
    {
        Group group = _service.AddGroup(new GroupName("M32071"), 10);
        Student student = _service.AddStudent(group, "Ivanov Ivan");
        Assert.True(student == _service.FindStudent(student.Id));
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group group = _service.AddGroup(new GroupName("M32071"), 10);
        Student student = _service.AddStudent(group, "Ivanov Ivan");
        Assert.Contains(student, _service.FindStudents(group.GroupName));
    }

    [Theory]
    [InlineData(10)]
    [InlineData(5)]
    [InlineData(20)]
    public void ReachMaxStudentPerGroup_ThrowException(int groupSize)
    {
        Group group = _service.AddGroup(new GroupName("A3100"), groupSize);
        for (int i = 0; i < groupSize; i++)
        {
            _service.AddStudent(group, "Ivanov Ivan");
        }

        Assert.Throws<GroupOverflowException>(() => _service.AddStudent(group, "Sergeev Sergey"));
    }

    [Theory]
    [InlineData("q329c")]
    [InlineData("l9999")]
    [InlineData("a55990")]
    [InlineData("M32081cc")]
    public void CreateGroupWithInvalidName_ThrowException(string groupName)
    {
        Assert.Throws<InvalidGroupNameException>(() => _service.AddGroup(new GroupName(groupName), 20));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group oldGroup = _service.AddGroup(new GroupName("A3100"), 20);
        Group newGroup = _service.AddGroup(new GroupName("A3114"), 20);

        Student student = _service.AddStudent(oldGroup, "Ivanov Ivan");
        _service.ChangeStudentGroup(student, newGroup);

        Assert.Contains(student, newGroup.Students);
        Assert.DoesNotContain(student, oldGroup.Students);
    }
}
