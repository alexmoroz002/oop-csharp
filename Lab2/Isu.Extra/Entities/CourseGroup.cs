using Isu.Entities;
using Isu.Extra.ExceptionsExtra;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class CourseGroup
{
    private const int StudentsLimit = 20;
    private StudentsList _students;
    private Timetable _timetable;

    public CourseGroup(int number, params Lesson[] lessons)
    {
        _students = new StudentsList(StudentsLimit);
        GroupNumber = number;
        _timetable = new Timetable(lessons);
    }

    public int GroupNumber { get; private set; }

    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public Timetable Timetable => _timetable;

    public void AddStudentInGroup(Student student)
    {
        if (_students.Exists(x => x == student))
            throw CourseGroupException.StudentAlreadyInGroup(student, this);
        _students.Add(student);
    }

    public void DeleteStudentFromGroup(Student student)
    {
        if (_students.RemoveAll(x => x == student) == 0)
            throw CourseGroupException.StudentNotFoundInGroup(student, this);
    }
}