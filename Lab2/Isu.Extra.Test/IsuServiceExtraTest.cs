using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.ExceptionsExtra;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuServiceExtraTest
{
    private IsuServiceExtra _service;

    public IsuServiceExtraTest()
    {
        _service = new IsuServiceExtra(new IsuService());
    }

    [Fact]
    public void AddNewCourse_CourseIsFound()
    {
        OgnpCourse course = _service.AddCourse('x', "CourseName");
        Assert.Contains(course, _service.Courses);
    }

    [Fact]
    public void AddFlowToCourse_FindFlow()
    {
        OgnpCourse course = _service.AddCourse('x', "kek");
        CourseFlow flow = _service.AddFlow(course);
        Assert.Contains(flow, _service.FindFlows(course));
    }

    [Fact]
    public void CreateLessonsAndAddInGroup_GroupHasTimetable()
    {
        Lesson[] lessons =
        {
            new Lesson(1, DayOfWeek.Friday, "Alex", 2232),
            new Lesson(1, DayOfWeek.Sunday, "Alex", 2232),
            new Lesson(1, DayOfWeek.Saturday, "Alex", 2232),
        };
        OgnpCourse course = _service.AddCourse('x', "CourseName");
        CourseFlow flow = _service.AddFlow(course);
        CourseGroup group = _service.AddGroup(flow, lessons);
        Assert.All(group.Timetable.Lessons, x => Assert.Contains(lessons, y => y == x));
    }

    [Fact]
    public void AddIntersectingLessons_ThrowException()
    {
        Lesson[] lessons =
        {
            new Lesson(1, DayOfWeek.Friday, "Alex", 2232),
            new Lesson(1, DayOfWeek.Friday, "Alex", 2232),
            new Lesson(1, DayOfWeek.Friday, "Alex", 2232),
        };
        OgnpCourse course = _service.AddCourse('x', "CourseName");
        CourseFlow flow = _service.AddFlow(course);
        Assert.Throws<TimetableException>(() => _service.AddGroup(flow, lessons));
    }

    [Fact]
    public void FindCourseByName_CourseIsFound()
    {
        OgnpCourse course = _service.AddCourse('x', "CourseName");
        Assert.Equal(course, _service.FindCourse(course.CourseName));
    }

    [Fact]
    public void AddStudentToOgnp_StudentSigned()
    {
        Group facultyGroup = _service.AddGroup(new GroupName("M3100"), 20);
        Student student = _service.AddStudent(facultyGroup, "Ivan");
        Lesson[] lessons =
        {
            new Lesson(1, DayOfWeek.Friday, "Alex", 2232),
            new Lesson(2, DayOfWeek.Friday, "Alex", 2232),
            new Lesson(3, DayOfWeek.Friday, "Alex", 2232),
        };

        Lesson[] lessonsCourse =
        {
            new Lesson(1, DayOfWeek.Saturday, "Alex", 2132),
            new Lesson(2, DayOfWeek.Saturday, "Alex", 2132),
            new Lesson(3, DayOfWeek.Saturday, "Alex", 2132),
        };

        _service.AddLessonsToGroup(facultyGroup, lessons);
        OgnpCourse course = _service.AddCourse('X', "CourseName");
        CourseFlow flow = _service.AddFlow(course);
        CourseGroup group = _service.AddGroup(flow, lessonsCourse);

        _service.AddRecordOnCourse(student, course, flow, group);
        Assert.Contains(student, group.Students);
        Assert.Contains(student, _service.FindStudents(course));
    }

    [Fact]
    public void AddCourseGroupWithoutLessons_ThrowException()
    {
        OgnpCourse course = _service.AddCourse('X', "CourseName");
        CourseFlow flow = _service.AddFlow(course);
        Assert.Throws<TimetableException>(() => _service.AddGroup(flow));
    }

    [Theory]
    [InlineData('1')]
    [InlineData('@')]
    public void AddOgnpCourseWithInvalidFaculty_ThrowException(char faculty)
    {
        Assert.Throws<OgnpCourseException>(() => _service.AddCourse(faculty, "Invalid"));
    }

    [Fact]
    public void AddRecordOnOgnpFromSameFaculty_ThrowException()
    {
        Group facultyGroup = _service.AddGroup(new GroupName("X3100"), 20);
        Student student = _service.AddStudent(facultyGroup, "Ivan");
        Lesson[] lessonsCourse =
        {
            new Lesson(1, DayOfWeek.Saturday, "Alex", 2132),
            new Lesson(2, DayOfWeek.Saturday, "Alex", 2132),
            new Lesson(3, DayOfWeek.Saturday, "Alex", 2132),
        };
        OgnpCourse course = _service.AddCourse('x', "CourseName");
        CourseFlow flow = _service.AddFlow(course);
        CourseGroup group = _service.AddGroup(flow, lessonsCourse);
        Assert.Throws<OgnpCourseException>(() => _service.AddRecordOnCourse(student, course, flow, group));
    }

    [Fact]
    public void AddRecordOnOgnp_LessonsIntersect_ThrowException()
    {
        Group facultyGroup = _service.AddGroup(new GroupName("M3100"), 20);
        Student student = _service.AddStudent(facultyGroup, "Ivan");
        Lesson[] lessons =
        {
            new Lesson(1, DayOfWeek.Friday, "Alex", 2232),
            new Lesson(2, DayOfWeek.Saturday, "Alex", 2232),
            new Lesson(3, DayOfWeek.Friday, "Alex", 2232),
        };

        Lesson[] lessonsCourse =
        {
            new Lesson(1, DayOfWeek.Saturday, "Alex", 2132),
            new Lesson(2, DayOfWeek.Saturday, "Alex", 2132),
            new Lesson(3, DayOfWeek.Saturday, "Alex", 2132),
        };

        _service.AddLessonsToGroup(facultyGroup, lessons);
        OgnpCourse course = _service.AddCourse('X', "CourseName");
        CourseFlow flow = _service.AddFlow(course);
        CourseGroup group = _service.AddGroup(flow, lessonsCourse);

        Assert.Throws<TimetableException>(() => _service.AddRecordOnCourse(student, course, flow, group));
    }

    [Fact]
    public void DeleteRecordOnCourse_StudentDoesntHaveCourse()
    {
        Group facultyGroup = _service.AddGroup(new GroupName("M3100"), 20);
        Student student = _service.AddStudent(facultyGroup, "Ivan");

        Lesson[] lessonsCourse =
        {
            new Lesson(1, DayOfWeek.Saturday, "Alex", 2132),
            new Lesson(2, DayOfWeek.Saturday, "Alex", 2132),
            new Lesson(3, DayOfWeek.Saturday, "Alex", 2132),
        };

        OgnpCourse course = _service.AddCourse('X', "CourseName");
        CourseFlow flow = _service.AddFlow(course);
        CourseGroup group = _service.AddGroup(flow, lessonsCourse);

        _service.AddRecordOnCourse(student, course, flow, group);
        _service.DeleteRecordOnCourse(student, course, flow, group);

        Assert.Contains(student, _service.FindUnsignedOgnpStudents(facultyGroup));
    }

    [Fact]
    public void AddSameLessonsToTwoGroups_ThrowException()
    {
        Group group1 = _service.AddGroup(new GroupName("M3100"), 20);
        Group group2 = _service.AddGroup(new GroupName("M3101"), 20);
        Lesson[] lessons =
        {
            new Lesson(1, DayOfWeek.Friday, "Alex", 2232),
            new Lesson(2, DayOfWeek.Saturday, "Alex", 2232),
            new Lesson(3, DayOfWeek.Friday, "Alex", 2232),
        };
        _service.AddLessonsToGroup(group1, lessons);
        Assert.Throws<LessonException>(() => _service.AddLessonsToGroup(group2, lessons));
    }

    [Fact]
    public void AddSameLessonsToGroupAndOgnp_ThrowException()
    {
        Group group = _service.AddGroup(new GroupName("M3100"), 20);
        Lesson[] lessons =
        {
            new Lesson(1, DayOfWeek.Friday, "Alex", 2232),
            new Lesson(2, DayOfWeek.Saturday, "Alex", 2232),
            new Lesson(3, DayOfWeek.Friday, "Alex", 2232),
        };
        _service.AddLessonsToGroup(group, lessons);
        OgnpCourse course = _service.AddCourse('X', "Course");
        CourseFlow flow = _service.AddFlow(course);
        Assert.Throws<LessonException>(() => _service.AddGroup(flow, lessons));
    }
}