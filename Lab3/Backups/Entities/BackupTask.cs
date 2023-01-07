using Backups.Interfaces;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    public BackupTask(IConfig config)
    {
        Backup = new Backup();
        Config = config ?? throw new ArgumentNullException("config");
    }

    public IConfig Config { get; }

    public IBackup Backup { get; }

    public RestorePoint CreateRestorePoint()
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