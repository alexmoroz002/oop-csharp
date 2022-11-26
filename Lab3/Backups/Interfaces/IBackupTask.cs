using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackupTask
{
    IConfig Config { get; }
    Backup Backup { get; }
    RestorePoint CreateRestorePoint();
    void CheckObjectsToBackup(params IBackupObject[] objects);
    void UncheckObjectsToBackup(params IBackupObject[] objects);
}