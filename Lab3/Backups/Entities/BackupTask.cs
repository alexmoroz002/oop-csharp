using Backups.Interfaces;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    public BackupTask(IConfig config)
    {
        Backup = new Backup();
        Config = config;
    }

    public IConfig Config { get; }
    public Backup Backup { get; }

    public RestorePoint CreateBackup()
    {
        return Backup.CreateRestorePoint(Config);
    }

    public void CheckObjectsToBackup(params IBackupObject[] objects)
    {
        Config.AddObjects(objects);
    }

    public void UncheckObjectsToBackup(params IBackupObject[] objects)
    {
        Config.RemoveObjects(objects);
    }
}