namespace Isu.Extra.Entities;

public class CourseFlow
{
    private List<CourseGroup> _groups;
    private int _lastId = 1;

    internal CourseFlow(int flowNumber)
    {
        _groups = new List<CourseGroup>();
        FlowNumber = flowNumber;
    }

    public int FlowNumber { get; private set; }
    public IReadOnlyList<CourseGroup> Groups => _groups;

    internal CourseGroup AddGroup(params Lesson[] lessons)
    {
        var group = new CourseGroup(_lastId++, lessons);
        _groups.Add(group);
        return group;
    }
}