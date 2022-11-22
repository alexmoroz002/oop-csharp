using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackupTask
{
    void CreateBackup();
    void CheckObjectsToBackup(params IBackupObject[] objects);
    void UncheckObjectsToBackup(params IBackupObject[] objects);
}