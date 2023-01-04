using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;
using Serilog;
using Zio;

namespace Backups.Extra.Entities;

public class BackupServiceExtra
{
    private List<IBackupTask> _backupTasks = new List<IBackupTask>();

    public BackupServiceExtra(bool loadState)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        if (loadState)
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
        RestorePoint rp = task.CreateRestorePoint();
        SaveState();
        return rp;
    }

    public void RestoreBackup(BackupTaskExtra task, RestorePoint restorePoint)
    {
        task.RestoreFromPoint(restorePoint);
        SaveState();
    }

    public void RestoreBackupTo(BackupTaskExtra task, RestorePoint restorePoint, Repository destRepo, UPath destFolder)
    {
        task.RestoreFromPointTo(restorePoint, destRepo, destFolder);
        SaveState();
    }

    public void AddObjectsToTask(IBackupTask task, params IBackupObject[] objects)
    {
        task.CheckObjectsToBackup(objects);
        SaveState();
    }

    public void RemoveObjectsFromTask(IBackupTask task, params IBackupObject[] objects)
    {
        task.UncheckObjectsToBackup(objects);
        SaveState();
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
        SaveState();
    }

    private void SaveState()
    {
        JsonSerializerSettings settings = new ()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        };

        string file = Path.Combine(Directory.GetCurrentDirectory(), "configuration.json");
        File.Delete(file);
        File.WriteAllText(file, JsonConvert.SerializeObject(_backupTasks, Formatting.Indented, settings));
    }

    private void LoadState()
    {
        JsonSerializerSettings settings = new ()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        };
        string file = Path.Combine(Directory.GetCurrentDirectory(), "configuration.json");
        if (!File.Exists(file))
            return;
        _backupTasks = JsonConvert.DeserializeObject<List<IBackupTask>>(File.ReadAllText(file), settings);
    }
}