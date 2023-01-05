using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;
using Serilog;
using Zio;

namespace Backups.Extra.Entities;

public class ConfigExtra : IConfig
{
    [JsonProperty("Config")]
    private readonly IConfig _config;

    public ConfigExtra(IConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        _config = config;
    }

    [JsonIgnore]
    public IAlgorithm Algorithm => _config.Algorithm;

    [JsonIgnore]
    public Repository Repository => _config.Repository;

    [JsonIgnore]
    public UPath BackupPath => _config.BackupPath;

    [JsonIgnore]
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