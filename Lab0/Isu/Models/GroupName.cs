using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private const string Pattern = @"^[A-Z](?:[3][1-4]|[4][1-2]|[5][1-6])\d{2}[1-9]?[c]?$";
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
            if (!Regex.IsMatch(value, Pattern))
                throw new InvalidGroupNameException("Group name is invalid");
            _name = value.ToUpper();
        }
    }
}