using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private Config _config;
    private Backup _backup;

    public BackupTask(Config config)
    {
        _config = config;
    }

    public void CreateBackup()
    {
        _config.
    }

    public void CheckObjectsToBackup(params BackupObject[] objects)
    {
        _config.AddObjects(objects);
    }

    public void UncheckObjectsToBackup(params BackupObject[] objects)
    {
        _config.DeleteObjects(objects);
    }
}