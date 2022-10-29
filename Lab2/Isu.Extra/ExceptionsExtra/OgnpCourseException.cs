using Isu.Entities;

namespace Isu.Extra.ExceptionsExtra;

public class OgnpCourseException : Exception
{
    private OgnpCourseException(string message)
        : base(message) { }

    public static OgnpCourseException InvalidFacultyLetter(char faculty)
    {
        return new OgnpCourseException($"Letter {faculty} is invalid faculty letter");
    }

    public static OgnpCourseException StudentAndOgnpFacultyIsSame(Student student, char faculty)
    {
        return new OgnpCourseException(
            $"Student {student.Name} can't sign up to Ognp course from his faculty {faculty}");
    }
}