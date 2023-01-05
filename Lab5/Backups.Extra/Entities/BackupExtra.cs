using Backups.Entities;
using Backups.Interfaces;
using Newtonsoft.Json;
using Serilog;

namespace Backups.Extra.Entities;

public class BackupExtra : IBackup
{
    [JsonProperty("Backup")]
    private readonly IBackup _backup = new Backup();

    [JsonIgnore]
    public int BackupVersion => _backup.BackupVersion;
    [JsonIgnore]
    public IReadOnlyList<RestorePoint> RestorePoints => _backup.RestorePoints;
    public RestorePoint CreateRestorePoint(IConfig config)
    {
        RestorePoint rp = _backup.CreateRestorePoint(config);
        return rp;
    }

    public void RemovePoints(params IRestorePoint[] points)
    {
        Log.Information("Removing {0} point(s)", points.Length);
        _backup.RemovePoints(points);
        Log.Information("Remove completed");
    }
}