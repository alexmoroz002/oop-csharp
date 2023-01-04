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
        Log.Information("Creating RP for {0}", string.Join(',', config.BackupObjects.Select(x => x.Name)));
        RestorePoint rp = _backup.CreateRestorePoint(config);
        Log.Information("RP created");
        return rp;
    }

    public void RemovePoints(params IRestorePoint[] points)
    {
        _backup.RemovePoints(points);
    }
}