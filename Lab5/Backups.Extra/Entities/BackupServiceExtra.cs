using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;
using Serilog;
using Zio;

namespace Backups.Extra.Entities;

public class BackupServiceExtra : IBackupServiceExtra
{
    private List<IBackupTask> _backupTasks = new List<IBackupTask>();

    public BackupServiceExtra(bool loadState = true)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        Log.Information("Initializing BackupServiceExtra");
        if (loadState)
            LoadState();
        else
            Log.Information("Configuration loading skipped");
    }

    public IBackupTaskExtra CreateTask(IConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        Log.Information("Adding new BackupTask to service");
        var task = new BackupTaskExtra(config);
        _backupTasks.Add(task);
        Log.Information("BackupTask added");
        SaveState();
        return task;
    }

    public IRestorePoint RunTask(IBackupTask task)
    {
        ArgumentNullException.ThrowIfNull(task);
        Log.Information(
            "Creating RP for {0} using {1} in repository {2} on path {3}",
            string.Join(',', task.Config.BackupObjects.Select(x => x.Name)),
            task.Config.Algorithm,
            task.Config.Repository,
            task.Config.BackupPath);
        RestorePoint rp = task.CreateRestorePoint();
        Log.Information("RP created");
        SaveState();
        return rp;
    }

    public void RestoreBackup(IBackupTaskExtra task, IRestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(task);
        Log.Information(
            "Restoring {0} from RP in {1}",
            string.Join(',', task.Config.BackupObjects.Select(x => x.Name)),
            task.Config.BackupPath);
        task.RestoreFromPoint(restorePoint);
        Log.Information("RP restored");
        SaveState();
    }

    public void RestoreBackupTo(IBackupTaskExtra task, IRestorePoint restorePoint, Repository destRepo, UPath destFolder)
    {
        ArgumentNullException.ThrowIfNull(task);
        Log.Information(
            "Restoring {0} from RP to {1} in {2}",
            string.Join(',', task.Config.BackupObjects.Select(x => x.Name)),
            destFolder,
            destRepo);
        task.RestoreFromPointTo(restorePoint, destRepo, destFolder);
        Log.Information("RP restored");
        SaveState();
    }

    public void AddObjectsToTask(IBackupTask task, params IBackupObject[] objects)
    {
        Log.Information("Adding {0} objects to BackupTask", string.Join(',', objects.Select(x => x.Name)));
        task.CheckObjectsToBackup(objects);
        Log.Information("Objects added");
        SaveState();
    }

    public void RemoveObjectsFromTask(IBackupTask task, params IBackupObject[] objects)
    {
        Log.Information("Removing {0} objects from BackupTask", string.Join(',', objects.Select(x => x.Name)));
        task.UncheckObjectsToBackup(objects);
        Log.Information("Objects removed");
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
                .WriteTo.File(path, outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}", shared: true)
                .CreateLogger();
        }
        else
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}", shared: true)
                .CreateLogger();
        }
    }

    public void PurgeRestorePoints(IBackupTaskExtra task)
    {
        Log.Information("Cleaning points in BackupTask");
        task.ClearPoints();
        Log.Information("Cleaning completed");
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
        Log.Information("Saving configuration to {0}", file);
        File.WriteAllText(file, JsonConvert.SerializeObject(_backupTasks, Formatting.Indented, settings));
        Log.Information("Saving completed");
    }

    private void LoadState()
    {
        string file = Path.Combine(Directory.GetCurrentDirectory(), "configuration.json");
        Log.Information("Loading configuration from {0}", file);
        JsonSerializerSettings settings = new ()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        };
        if (!File.Exists(file))
        {
            Log.Information("Configuration file in {0} did not found", file);
            return;
        }

        _backupTasks = JsonConvert.DeserializeObject<List<IBackupTask>>(File.ReadAllText(file), settings);
        Log.Information("Loading completed");
    }
}