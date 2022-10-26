using Isu.Models;

namespace Isu.Extra.Entities;

public class Group
{
    private const int StudentsLimit = 20;
    private StudentsList _students;
    public int GroupNumber { get; private set; }
}