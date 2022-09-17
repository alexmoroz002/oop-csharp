using Isu.Entities;
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
    }

    [Theory]
    [InlineData(10, 5)]
    [InlineData(5, 6)]
    [InlineData(6, 6)]
    public void ReachMaxStudentPerGroup_ThrowException(int groupSize, int studentsCount)
    {
        var service = new IsuService();
        Group group = service.AddGroup(new GroupName("M32071"), groupSize);
        for (int i = 0; i < studentsCount; i++)
        {
            service.AddStudent(group, "Ivan Ivanov");
        }
    }

    [Theory]
    [InlineData("M32071")]
    [InlineData("M32071c")]
    [InlineData("q329c")]
    [InlineData("l9999")]
    [InlineData("a5599")]
    public void CreateGroupWithInvalidName_ThrowException(string groupName)
    {
        var service = new IsuService();
        service.AddGroup(new GroupName(groupName));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        IsuService service = new IsuService();
    }

    /*
     * [Theory]
     * [InlineData]
     */
}