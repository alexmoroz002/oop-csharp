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
            if (value is < 1 or > 8)
                return;
            _year = value;
        }
    }
}