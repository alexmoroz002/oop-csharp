using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Serilog;
using Zio;

namespace Backups.Extra.Entities;

public class BackupServiceExtra
{
    private List<IBackupTask> _backupTasks = new List<IBackupTask>();

    public BackupServiceExtra(ILogger logger)
    {
        Log.Logger = logger;
        LoadState();
    }

    public BackupServiceExtra()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        LoadState();
    }

    public BackupTaskExtra CreateTask(IConfig config)
    {
        var task = new BackupTaskExtra(config);
        _backupTasks.Add(task);
        SaveState();
        return task;
    }

    public RestorePoint RunTask(IBackupTask task)
    {
        return task.CreateRestorePoint();
    }

    public void RestoreBackup(BackupTaskExtra task, RestorePoint restorePoint)
    {
        task.RestoreFromPoint(restorePoint);
    }

    public void RestoreBackupTo(BackupTaskExtra task, RestorePoint restorePoint, Repository destRepo, UPath destFolder)
    {
        task.RestoreFromPointTo(restorePoint, destRepo, destFolder);
    }

    public void AddObjectsToTask(IBackupTask task, params IBackupObject[] objects)
    {
        task.CheckObjectsToBackup(objects);
    }

    public void RemoveObjectsFromTask(IBackupTask task, params IBackupObject[] objects)
    {
        task.UncheckObjectsToBackup(objects);
    }

    public void LogToConsole(bool addTimeStamp)
    {
        if (!addTimeStamp)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
        else
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }

    public void LogToFile(string path, bool addTimeStamp)
    {
        if (!addTimeStamp)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path, outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
        else
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }

    public void PurgeRestorePoints(BackupTaskExtra task)
    {
        task.CleanPoints();
    }

    private void SaveState()
    {
    }

    private void LoadState()
    {
    }
}