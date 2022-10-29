using Isu.Entities;
using Isu.Extra.Decorators;
using Isu.Extra.Entities;
using Isu.Extra.ExceptionsExtra;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuServiceExtra : IsuBaseDecorator, IIsuServiceExtra
{
    private List<OgnpCourse> _courses;
    private Dictionary<Student, Timetable> _studentTimetable;

    public IsuServiceExtra(IIsuService wrapper)
        : base(wrapper)
    {
        _studentTimetable = new Dictionary<Student, Timetable>();
        _courses = new List<OgnpCourse>();
    }

    public IReadOnlyList<OgnpCourse> Courses => _courses;

    public OgnpCourse AddCourse(char faculty, string name)
    {
        if (!char.IsLetter(faculty))
            throw OgnpCourseException.InvalidFacultyLetter(faculty);
        var course = new OgnpCourse(faculty, name);
        _courses.Add(course);
        return course;
    }

    public CourseFlow AddFlow(OgnpCourse course)
    {
        return course.AddFlow();
    }

    public CourseGroup AddGroup(CourseFlow flow, params Lesson[] lessons)
    {
        return flow.AddGroup(lessons);
    }

    public void AddRecordOnCourse(Student student, OgnpCourse course, CourseFlow flow, CourseGroup group)
    {
        CourseGroup courseGroup = FindGroups(course, flow).FirstOrDefault(x => x == group);
        if (courseGroup == null)
            throw CourseGroupException.GroupNull();
        Group facultyGroup = default;
        for (int i = 1; i < 6; i++)
        {
            IReadOnlyList<Group> groups = FindGroups(new CourseNumber(i));
            facultyGroup = groups.FirstOrDefault(x => x.Students.Contains(student));
            if (facultyGroup != default)
                break;
        }

        if (facultyGroup == null)
            throw CourseGroupException.GroupNull();
        if (facultyGroup.GroupName.Name[0] == course.FacultyLetter)
            throw OgnpCourseException.StudentAndOgnpFacultyIsSame(student, course.FacultyLetter);

        if (!_studentTimetable.ContainsKey(student))
            _studentTimetable.Add(student, new Timetable());
        if (_studentTimetable[student].Lessons.Intersect(courseGroup.Timetable.Lessons).Any())
            throw TimetableException.TimetableIntersection();

        courseGroup.AddStudentInGroup(student);
        Lesson[] lessons = courseGroup.Timetable.Lessons.ToArray();
        _studentTimetable[student].AddLessons(lessons);
    }

    public void DeleteRecordOnCourse(Student student, OgnpCourse course, CourseFlow flow, CourseGroup group)
    {
        CourseGroup courseGroup = FindGroups(course, flow).FirstOrDefault(x => x == group);
        if (courseGroup == null)
            throw CourseGroupException.GroupNull();
        courseGroup.DeleteStudentFromGroup(student);
        _studentTimetable[student].DeleteLessons(courseGroup.Timetable.Lessons.ToArray());
    }

    public IReadOnlyList<CourseFlow> FindFlows(OgnpCourse course)
    {
        return _courses.FirstOrDefault(x => x == course)?.Flows ?? new List<CourseFlow>().AsReadOnly();
    }

    public IReadOnlyList<Student> FindStudents(OgnpCourse course)
    {
        return course.Flows
            .SelectMany(courseFlow => courseFlow.Groups, (courseFlow, courseFlowGroup) => new { courseFlow, courseFlowGroup })
            .SelectMany(@t => @t.courseFlowGroup.Students).ToList();
    }

    public IReadOnlyList<Student> FindUnsignedOgnpStudents(Group group)
    {
        return group.Students.Where(groupStudent => CountSignedOgnp(groupStudent) < 1).ToList();
    }

    public OgnpCourse FindCourse(string courseName)
    {
        return _courses.Find(x => x.CourseName == courseName);
    }

    public IReadOnlyList<CourseGroup> FindGroups(OgnpCourse course, CourseFlow flow)
    {
        return FindFlows(course).FirstOrDefault(x => x == flow)?.Groups ?? new List<CourseGroup>().AsReadOnly();
    }

    public void AddLessonsToGroup(Group group, params Lesson[] lessons)
    {
        foreach (Lesson lesson in lessons)
        {
            lesson.SetGroup(group.GroupName.Name);
        }

        foreach (Student groupStudent in group.Students)
        {
            if (!_studentTimetable.ContainsKey(groupStudent))
            {
                _studentTimetable.Add(groupStudent, new Timetable(lessons));
            }
            else
            {
                if (_studentTimetable[groupStudent].Lessons.Intersect(lessons).Any())
                    throw TimetableException.TimetableIntersection();
                _studentTimetable[groupStudent].AddLessons(lessons);
            }
        }
    }

    private int CountSignedOgnp(Student student)
    {
        return _courses
            .Sum(ognpCourse => ognpCourse.Flows
                .Sum(ognpCourseFlow => ognpCourseFlow.Groups
                    .Count(courseGroup => courseGroup.Students
                        .Any(x => x == student))));
    }
}