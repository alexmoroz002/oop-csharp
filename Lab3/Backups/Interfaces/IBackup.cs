using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackup
{
    int BackupVersion { get; }
    IReadOnlyList<RestorePoint> RestorePoints { get; }
    RestorePoint CreateRestorePoint(IConfig config);
}