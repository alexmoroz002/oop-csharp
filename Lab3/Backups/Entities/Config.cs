using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Entities;

public class Config : IConfig
{
    private List<IBackupObject> _trackedObjects;

    public Config(IAlgorithm algorithm, Repository repository, UPath backupPath)
    {
        Algorithm = algorithm;
        Repository = repository;
        _trackedObjects = new List<IBackupObject>();
        if (!backupPath.IsAbsolute)
            throw ConfigException.PathIsRelativeException(backupPath);
        BackupPath = backupPath;
    }

    public IAlgorithm Algorithm { get; }

    public Repository Repository { get; }
    public UPath BackupPath { get; }
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
}