using Isu.Extra.Decorators;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuServiceOgnp : IsuDecorator
{
    public IsuServiceOgnp(IIsuService wrapper)
        : base(wrapper) { }
}