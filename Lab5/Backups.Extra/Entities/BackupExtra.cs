using Backups.Entities;
using Backups.Interfaces;
using Serilog;

namespace Backups.Extra.Entities;

public class BackupExtra : IBackup
{
    private readonly IBackup _backup = new Backup();

    public int BackupVersion => _backup.BackupVersion;
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