using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Services;

public interface IIsuServiceExtra
{
    public OgnpCourse AddCourse(char faculty, string name);
    public CourseFlow AddFlow(OgnpCourse course);
    public CourseGroup AddGroup(CourseFlow flow, params Lesson[] lessons);
    public void AddRecordOnCourse(Student student, OgnpCourse course, CourseFlow flow, CourseGroup group);
    public void DeleteRecordOnCourse(Student student, OgnpCourse course, CourseFlow flow, CourseGroup group);
    public IReadOnlyList<CourseFlow> FindFlows(OgnpCourse course);
    public IReadOnlyList<Student> FindStudents(OgnpCourse course);
    public OgnpCourse FindCourse(string courseName);
    public IReadOnlyList<Student> FindUnsignedOgnpStudents(Group group);
    public IReadOnlyList<CourseGroup> FindGroups(OgnpCourse course, CourseFlow flow);
}