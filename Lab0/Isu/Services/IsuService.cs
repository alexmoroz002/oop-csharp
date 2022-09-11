using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

internal class IsuService : IIsuService
{
    private List<Group> _groups;

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

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(x => x.GroupName == groupName);
    }
}
