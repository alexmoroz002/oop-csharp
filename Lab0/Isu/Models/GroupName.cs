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
            const string pattern = @"^([A-Z]([3][1-4]|[4][1-2]|[5][1-6])([0-9][0-9])([1-9]?[c]?))$";
            if (!Regex.IsMatch(value, pattern))
                throw new InvalidGroupNameException("Group name is invalid");
            _name = value.ToUpper();
        }
    }
}