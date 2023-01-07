using Backups.Interfaces;
using Newtonsoft.Json;

namespace Backups.Entities;

public class Backup : IBackup
{
    [JsonProperty("RestorePointList")]
    private List<RestorePoint> _restorePoints;

    public Backup()
    {
        BackupVersion = 1;
        _restorePoints = new List<RestorePoint>();
    }

    public int BackupVersion { get; private set; }

    [JsonIgnore]
    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public RestorePoint CreateRestorePoint(IConfig config)
    {
        if (config == null)
            throw new ArgumentNullException("config");
        List<Storage> storages = config.Algorithm.ArchiveObject(config.Repository, config.BackupPath, BackupVersion, config.BackupObjects);
        BackupVersion++;
        var restorePoint = new RestorePoint(config.BackupObjects.ToList(), storages);
        _restorePoints.Add(restorePoint);
        return restorePoint;
    }

    public void RemovePoints(params IRestorePoint[] points)
    {
        _restorePoints.RemoveAll(points.Contains);
    }
}