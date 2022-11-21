using Backups.Models;

namespace Backups.Interfaces;

public interface IBackupTask
{
    void CreateBackup();
    void CheckObjectsToBackup(params BackupObject[] objects);
    void UncheckObjectsToBackup(params BackupObject[] objects);
}