namespace Isu.Extra.Models;

public abstract class Course
{
    protected internal string Name { get; protected set; }
    protected abstract void AddFlow();
    protected abstract void DeleteEmptyFlowS();
}