using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Services;

public interface IIsuServiceExtra
{
     OgnpCourse AddCourse(char faculty, string name);
     CourseFlow AddFlow(OgnpCourse course);
     CourseGroup AddGroup(CourseFlow flow, params Lesson[] lessons);
     void AddRecordOnCourse(Student student, OgnpCourse course, CourseFlow flow, CourseGroup group);
     void DeleteRecordOnCourse(Student student, OgnpCourse course, CourseFlow flow, CourseGroup group);
     IReadOnlyList<CourseFlow> FindFlows(OgnpCourse course);
     IReadOnlyList<Student> FindStudents(OgnpCourse course);
     OgnpCourse FindCourse(string courseName);
     IReadOnlyList<Student> FindUnsignedOgnpStudents(Group group);
     IReadOnlyList<CourseGroup> FindGroups(OgnpCourse course, CourseFlow flow);
     void AddLessonsToGroup(Group group, params Lesson[] lessons);
}