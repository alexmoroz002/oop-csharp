using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class CountLimitAlgorithm : ILimitAlgorithm
{
    private int _limit;

    public CountLimitAlgorithm(int limit)
    {
        _limit = limit;
    }

    public IEnumerable<IRestorePoint> Execute(IEnumerable<IRestorePoint> points)
    {
        return points.SkipLast(_limit).Distinct();
    }
}