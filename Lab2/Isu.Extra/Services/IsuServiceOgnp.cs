using Isu.Extra.Decorators;
using Isu.Extra.Entities;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuServiceOgnp : IsuDecorator
{
    private List<Teacher> _teachers;

    public IsuServiceOgnp(IIsuService wrapper)
        : base(wrapper) { }
}