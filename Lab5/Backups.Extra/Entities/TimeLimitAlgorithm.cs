using Backups.Interfaces;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;

public class TimeLimitAlgorithm : ILimitAlgorithm
{
    [JsonProperty("SaveDataLimit")]
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