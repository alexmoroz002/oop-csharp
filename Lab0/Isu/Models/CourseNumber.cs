namespace Isu.Models;

public class CourseNumber
{
    private int _year;

    public int Course
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