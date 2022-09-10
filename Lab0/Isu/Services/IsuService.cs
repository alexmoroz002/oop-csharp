// using Isu.Entities;
// using Isu.Models;

using Isu.Entities;

namespace Isu.Services;

internal class IsuService : IIsuService
{
    private List<Group> _groups;

    public IsuService()
    {
        _groups = new List<Group>();
    }
}
