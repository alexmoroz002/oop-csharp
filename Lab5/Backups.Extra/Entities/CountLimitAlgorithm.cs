using Backups.Interfaces;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;

public class CountLimitAlgorithm : ILimitAlgorithm
{
    [JsonProperty("Limit")]
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