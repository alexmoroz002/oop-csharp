using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class TimeLimitAlgorithm : ILimitAlgorithm
{
    private DateTime _saveDateTime;

    public TimeLimitAlgorithm(DateTime saveDateTime)
    {
        _saveDateTime = saveDateTime;
    }

    public IEnumerable<IRestorePoint> Execute(IEnumerable<IRestorePoint> points)
    {
        return points.Where(x => x.CreationTime < _saveDateTime).Distinct();
    }
}