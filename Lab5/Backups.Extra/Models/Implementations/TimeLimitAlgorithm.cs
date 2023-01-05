using Backups.Extra.Entities;
using Backups.Interfaces;
using Newtonsoft.Json;
using Serilog;

namespace Backups.Extra.Models.Implementations;

public class TimeLimitAlgorithm : ILimitAlgorithm
{
    [JsonProperty("SaveDataLimit")]
    private DateTime _saveDateTime;

    public TimeLimitAlgorithm(DateTime saveDateTime)
    {
        Log.Information("Creating new TimeLimitAlgorithm with {0} date limit", saveDateTime);
        ArgumentNullException.ThrowIfNull(saveDateTime);
        _saveDateTime = saveDateTime;
        Log.Information("Algorithm created");
    }

    public IEnumerable<IRestorePoint> Execute(IEnumerable<IRestorePoint> points)
    {
        Log.Information("Applying TimeLimitAlgorithm with {0} date limit", _saveDateTime);
        ArgumentNullException.ThrowIfNull(points);
        IEnumerable<IRestorePoint> result = points.Where(x => x.CreationTime < _saveDateTime).Distinct();
        Log.Information("Algorithm executed successfully");
        return result;
    }
}