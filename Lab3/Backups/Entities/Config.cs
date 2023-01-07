using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;
using Zio;

namespace Backups.Entities;

public class Config : IConfig
{
    [JsonProperty("TrackedObjectList")]
    private List<IBackupObject> _trackedObjects;

    public Config(IAlgorithm algorithm, Repository repository, UPath backupPath)
    {
        Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        if (backupPath == null)
            throw new ArgumentNullException(nameof(backupPath));
        _trackedObjects = new List<IBackupObject>();
        if (!backupPath.IsAbsolute)
            throw ConfigException.PathIsRelativeException(backupPath);
        BackupPath = backupPath;
    }

    public IAlgorithm Algorithm { get; private set; }

    public Repository Repository { get; }

    [JsonConverter(typeof(UPathConverter))]
    public UPath BackupPath { get; }

    [JsonIgnore]
    public IReadOnlyList<IBackupObject> BackupObjects => _trackedObjects;

    public void AddObjects(params IBackupObject[] objects)
    {
        if (objects == null)
        {
            throw ConfigException.NullException();
        }

        if (objects.Distinct().Count() < objects.Length)
            throw ConfigException.ObjectNameException();

        _trackedObjects.AddRange(objects);
    }

    public void RemoveObjects(params IBackupObject[] objects)
    {
        if (objects == null)
        {
            throw ConfigException.NullException();
        }

        _trackedObjects = _trackedObjects.Except(objects).ToList();
    }

    public void ChangeBackupAlgorithm(IAlgorithm newAlgorithm)
    {
        Algorithm = newAlgorithm;
    }
}