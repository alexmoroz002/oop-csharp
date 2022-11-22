using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private Config _config;
    private Backup _backup;
    private string _name;

    public BackupTask(string name, Config config)
    {
        _backup = new Backup();
        _config = config;
        _name = name;
    }

    public BackupTask(string name, Repository repository, IAlgorithm algorithm)
    {
        _backup = new Backup();
        _config = new Config(algorithm, repository);
        _name = name;
    }

    public RestorePoint CreateBackup()
    {
        return _backup.CreateRestorePoint();
    }

    public void CheckObjectsToBackup(params IBackupObject[] objects)
    {
        _config.AddObjects(objects);
    }

    public void UncheckObjectsToBackup(params IBackupObject[] objects)
    {
        _config.DeleteObjects(objects);
    }
}