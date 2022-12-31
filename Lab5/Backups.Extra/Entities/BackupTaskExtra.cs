using Backups.Entities;
using Backups.Interfaces;
using Serilog;
using Zio;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : IBackupTask
{
    private readonly IBackupTask _backupTask;

    public BackupTaskExtra(IConfig config)
    {
        // Config = config;
        Backup = new BackupExtra();
        _backupTask = new BackupTask(config);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("C:\\1\\123.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }

    public IConfig Config => _backupTask.Config;
    public IBackup Backup { get; }

    public RestorePoint CreateRestorePoint()
    {
        return Backup.CreateRestorePoint(_backupTask.Config);
    }

    public void RestoreFromPoint(RestorePoint rp, UPath path)
    {
        foreach (Storage storage in rp.Storages)
        {
            if (_backupTask.Config.Repository.)
                _backupTask.Config.Repository.
        }
    }

    public void CheckObjectsToBackup(params IBackupObject[] objects)
    {
        _backupTask.CheckObjectsToBackup(objects);
    }

    public void UncheckObjectsToBackup(params IBackupObject[] objects)
    {
        _backupTask.UncheckObjectsToBackup(objects);
    }
}