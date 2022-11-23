using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackupTask
{
    RestorePoint CreateBackup();
    void CheckObjectsToBackup(params IBackupObject[] objects);
    void UncheckObjectsToBackup(params IBackupObject[] objects);
}