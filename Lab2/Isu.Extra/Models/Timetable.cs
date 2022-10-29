using Isu.Extra.Entities;
using Isu.Extra.ExceptionsExtra;

namespace Isu.Extra.Models;

public class Timetable
{
    private List<Lesson> _lessons = new ();

    public Timetable() { }

    internal Timetable(params Lesson[] lessons)
    {
        if (lessons.Length < 1)
            throw TimetableException.EmptyLessonList();
        if (lessons.Distinct().Count() < lessons.Length)
            throw TimetableException.TimetableIntersection();
        foreach (Lesson lesson in lessons)
        {
            if (lesson == null)
                throw LessonException.NullLesson();
            _lessons.Add(lesson);
        }
    }

    public IReadOnlyList<Lesson> Lessons => _lessons;

    internal void AddLessons(params Lesson[] lessons)
    {
        foreach (Lesson lesson in lessons)
        {
            _lessons.Add(lesson);
        }
    }

    internal void DeleteLessons(params Lesson[] lessons)
    {
        foreach (Lesson lesson in lessons)
        {
            _lessons.Remove(lesson);
        }
    }
}