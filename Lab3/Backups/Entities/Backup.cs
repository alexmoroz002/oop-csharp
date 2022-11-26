using Backups.Interfaces;

namespace Backups.Entities;

public class Backup : IBackup
{
    private List<RestorePoint> _restorePoints;

    public Backup()
    {
        BackupVersion = 1;
        _restorePoints = new List<RestorePoint>();
    }

    public int BackupVersion { get; private set; }
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
}