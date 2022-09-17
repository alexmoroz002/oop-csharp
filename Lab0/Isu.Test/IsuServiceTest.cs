using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent() { }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException() { }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException() { }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        IIsuService service = new IsuService();
    }

    /*
     * [Theory]
     * [InlineData]
     */
}