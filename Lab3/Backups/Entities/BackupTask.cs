using Backups.Interfaces;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private IConfig _config;
    private Backup _backup;
    private string _name;

    public BackupTask(string name, IConfig config)
    {
        _backup = new Backup();
        _config = config;
        _name = name;
    }

    public RestorePoint CreateBackup()
    {
        return _backup.CreateRestorePoint(_config);
    }

    public void CheckObjectsToBackup(params IBackupObject[] objects)
    {
        _config.AddObjects(objects);
    }

    public void UncheckObjectsToBackup(params IBackupObject[] objects)
    {
        _config.RemoveObjects(objects);
    }
}