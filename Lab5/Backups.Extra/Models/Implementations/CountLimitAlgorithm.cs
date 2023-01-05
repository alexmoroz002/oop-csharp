using Backups.Extra.Entities;
using Backups.Interfaces;
using Newtonsoft.Json;
using Serilog;

namespace Backups.Extra.Models.Implementations;

public class CountLimitAlgorithm : ILimitAlgorithm
{
    [JsonProperty("Limit")]
    private int _limit;

    public CountLimitAlgorithm(int limit)
    {
        Log.Information("Creating new CountLimitAlgorithm with {0} RP limit", limit);
        ArgumentNullException.ThrowIfNull(limit);
        _limit = limit;
        Log.Information("Algorithm created");
    }

    public IEnumerable<IRestorePoint> Execute(IEnumerable<IRestorePoint> points)
    {
        Log.Information("Applying CountLimitAlgorithm with {0} RP limit", _limit);
        ArgumentNullException.ThrowIfNull(points);
        IEnumerable<IRestorePoint> result = points.SkipLast(_limit).Distinct();
        Log.Information("Algorithm executed successfully");
        return result;
    }
}