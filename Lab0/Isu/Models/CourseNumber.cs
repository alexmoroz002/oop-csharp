using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    private int _year;

    public CourseNumber(int year)
    {
        Year = year;
    }

    public int Year
    {
        get => _year;
        private set
        {
            const int bachelorFirstCourse = 1;
            const int specialtyLastCourse = 5;

            if (value is < bachelorFirstCourse or > specialtyLastCourse)
                throw new InvalidCourseNumberException("Course number is invalid");
            _year = value;
        }
    }
}