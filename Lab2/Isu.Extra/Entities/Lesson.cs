namespace Isu.Extra.Entities;

public class Lesson
{
    private static readonly Dictionary<int, TimeOnly> LessonNumberToTime = new ()
    {
        { 1, new TimeOnly(8, 20) },
        { 2, new TimeOnly(10, 00) },
        { 3, new TimeOnly(11, 40) },
        { 4, new TimeOnly(13, 30) },
        { 5, new TimeOnly(15, 20) },
        { 6, new TimeOnly(17, 00) },
        { 7, new TimeOnly(18, 40) },
        { 8, new TimeOnly(20, 20) },
    };
    private TimeOnly _startTime;

    public Lesson(int lessonNumber, DayOfWeek dayOfWeek, int teacherId, int classroom)
    {
        if (LessonNumberToTime.TryGetValue(lessonNumber, out _startTime))
            throw new NotImplementedException();
        DayOfWeek = dayOfWeek;

        // Group = group;
        TeacherId = teacherId;
        Classroom = classroom;
    }

    public TimeOnly StartTime => _startTime;
    public DayOfWeek DayOfWeek { get; }

    // public Group Group { get; }
    public int TeacherId { get; }
    public int Classroom { get; }
}