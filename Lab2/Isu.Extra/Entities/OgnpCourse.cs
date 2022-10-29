namespace Isu.Extra.Entities;

public class OgnpCourse
{
    private List<CourseFlow> _flows;
    private int _lastId = 1;

    public OgnpCourse(char facultyLetter, string courseName)
    {
        _flows = new List<CourseFlow>();
        FacultyLetter = char.ToUpper(facultyLetter);
        CourseName = courseName;
    }

    public IReadOnlyList<CourseFlow> Flows => _flows.AsReadOnly();

    public char FacultyLetter { get; private set; }

    public string CourseName { get; private set; }

    internal CourseFlow AddFlow()
    {
        var flow = new CourseFlow(_lastId++);
        _flows.Add(flow);
        return flow;
    }
}