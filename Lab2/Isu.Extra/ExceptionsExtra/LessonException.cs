namespace Isu.Extra.ExceptionsExtra;

public class LessonException : Exception
{
    private LessonException(string message)
        : base(message) { }

    public static LessonException InvalidLessonNumber(int number)
    {
        return new LessonException($"{number} is invalid");
    }

    public static LessonException NullLesson()
    {
        return new LessonException("Lesson is null");
    }
}