using System.Text.RegularExpressions;
using Isu.Exceptions;

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
                    @"^([A-Z]([3][1-4]|[4][1-2]|[5][1-6])([0-9][0-9])([1-9]?[c]?))$",
                    RegexOptions.IgnoreCase);
            if (!patternRegex.IsMatch(value))
                throw new InvalidGroupNameException(" ");
            _name = value.ToUpper();
        }
    }
}