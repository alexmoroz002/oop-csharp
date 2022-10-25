using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public abstract class Course
{
    internal List<Flow> Flows { get; set; }
}