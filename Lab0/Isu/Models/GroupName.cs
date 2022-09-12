using System.Text.RegularExpressions;

namespace Isu.Models;

public class GroupName
{
    private string _name = string.Empty;

    public GroupName(string name)
    {
        Name = name;
    }

    private enum EducationLevel
    {
        Bachelor = 3,
        Magistracy = 4,
        Specialty = 5,
        Graduate = 7,
        Doctoral = 8,
    }

    public string Name
    {
        get => _name;

        private set
        {
            var patternRegex = new Regex(
                    @"^(([A-Z][3][1-4]|[A-Z][4][1-2]|[B][5][1-6]|[7-8][0-9])([0-9][0-9])([1-4]?[c]?))$",
                    RegexOptions.IgnoreCase);
            if (!patternRegex.IsMatch(value))
                return;
            _name = value.ToUpper();
        }
    }
}