using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OgnpCourse : Course
{
    protected internal List<OgnpFlow> Flows { get; protected set; }
    private string Faculty { get; set; }

    protected override void AddFlow()
    {
        throw new NotImplementedException();
    }

    protected override void DeleteEmptyFlowS()
    {
        throw new NotImplementedException();
    }
}