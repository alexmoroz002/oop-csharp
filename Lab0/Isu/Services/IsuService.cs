using Isu.Entities;

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
            Student? foundStudent = group.Students.Find(x => x.Id == id);
            if (foundStudent != null)
                return foundStudent;
        }

        return null;
    }
}
