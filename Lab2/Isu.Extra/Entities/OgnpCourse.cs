namespace Isu.Extra.Entities;

public class OgnpCourse
{
    private List<Flow> _flows;
    public IReadOnlyList<Flow> Flows => _flows.AsReadOnly();
    public string Faculty { get; private set; }
    public string Name { get; private set; }

    protected void AddFlow()
    {
        throw new NotImplementedException();
    }

    protected void DeleteEmptyFlowS()
    {
        throw new NotImplementedException();
    }
}