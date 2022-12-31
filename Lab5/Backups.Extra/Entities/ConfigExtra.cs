using Backups.Interfaces;
using Backups.Models;
using Serilog;
using Zio;

namespace Backups.Extra.Entities;

public class ConfigExtra : IConfig
{
    private readonly IConfig _config;

    public ConfigExtra(IConfig config)
    {
        _config = config;
    }

    public IAlgorithm Algorithm => _config.Algorithm;
    public Repository Repository => _config.Repository;
    public UPath BackupPath => _config.BackupPath;
    public IReadOnlyList<IBackupObject> BackupObjects => _config.BackupObjects;
    public void AddObjects(params IBackupObject[] objects)
    {
        Log.Information("Selecting {0} to backup", string.Join(',', objects.Select(x => x.Name)));
        _config.AddObjects(objects);
        Log.Information("Objects selected");
    }

    public void RemoveObjects(params IBackupObject[] objects)
    {
        Log.Information("Unselecting {0} to backup", string.Join(',', objects.Select(x => x.Name)));
        _config.RemoveObjects(objects);
        Log.Information("Objects unselected");
    }

    public void ChangeBackupAlgorithm(IAlgorithm newAlgorithm)
    {
        Log.Information("Changing backup algorithm");
        _config.ChangeBackupAlgorithm(newAlgorithm);
        Log.Information("Algorithm changed");
    }
}