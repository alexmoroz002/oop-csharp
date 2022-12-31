using Backups.Entities;
using Backups.Interfaces;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;

namespace Backups.Extra.Entities;

public class BackupServiceExtra
{
    private List<IBackupTask> _backupTasks = new List<IBackupTask>();

    public BackupServiceExtra(ILogger logger)
    {
        Log.Logger = logger;
        LoadState();
    }

    public IBackupTask CreateTask(IConfig config)
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

    private void SaveState()
    {
    }

    private void LoadState()
    {
    }
}