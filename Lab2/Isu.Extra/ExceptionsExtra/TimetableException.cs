namespace Isu.Extra.ExceptionsExtra;

public class TimetableException : Exception
{
    private TimetableException(string message)
        : base(message) { }

    public static TimetableException EmptyLessonList()
    {
        return new TimetableException("Lesson list is empty");
    }

    public static TimetableException TimetableIntersection()
    {
        return new TimetableException("Lessons intersect with students timetable");
    }
}