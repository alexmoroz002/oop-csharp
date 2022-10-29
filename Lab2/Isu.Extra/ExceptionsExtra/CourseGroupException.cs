using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.ExceptionsExtra;

public class CourseGroupException : Exception
{
    private CourseGroupException(string message)
        : base(message) { }

    public static CourseGroupException StudentAlreadyInGroup(Student student, CourseGroup group)
    {
        return new CourseGroupException($"Student {student.Name} is already in group {group.GroupNumber}");
    }

    public static CourseGroupException StudentNotFoundInGroup(Student student, CourseGroup group)
    {
        return new CourseGroupException($"Student {student} wasn't found in group {group.GroupNumber}");
    }

    public static CourseGroupException GroupNull()
    {
        return new CourseGroupException("Group is null");
    }
}