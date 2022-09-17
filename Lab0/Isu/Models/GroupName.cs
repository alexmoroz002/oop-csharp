using System.Text.RegularExpressions;

namespace Isu.Models;

public class GroupName
{
    private string _name = string.Empty;

    public GroupName(string name)
    {
        Name = name;
    }

    public string Name
    {
        get => _name;

        private set
        {
            var patternRegex = new Regex(
                    @"^([A-Z]([3][1-4]|[4][1-2]|[5][1-6])([0-9][0-9])([1-4]?[c]?))$",
                    RegexOptions.IgnoreCase);
            if (!patternRegex.IsMatch(value))
                return;
            _name = value.ToUpper();
        }
    }
}