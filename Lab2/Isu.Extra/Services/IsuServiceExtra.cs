using Isu.Extra.Decorators;
using Isu.Extra.Entities;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuServiceExtra : IsuDecorator, IIsuServiceExtra
{
    private List<Teacher> _teachers;

    public IsuServiceExtra(IIsuService wrapper)
        : base(wrapper) { }
}