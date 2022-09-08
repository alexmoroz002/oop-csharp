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
                    @"^(([BDGHK-PRT-WZ][3][1-4]|[A-HJ-WY][4][1-2]|[B][5][1-6]|[7-8][6-9])([0-9][0-9])([1-4]?[c]?))$",
                    RegexOptions.IgnoreCase);
            if (!patternRegex.IsMatch(value))
                return;
            _name = value.ToUpper();
        }
    }
}