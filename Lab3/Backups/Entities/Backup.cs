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

    public int BackupVersion { get; }

    public RestorePoint CreateRestorePoint(IConfig config)
    {
        throw new NotImplementedException();
    }
}