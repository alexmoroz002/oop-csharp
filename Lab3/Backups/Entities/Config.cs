using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class Config : IConfig
{
    private List<IBackupObject> _trackedObjects;

    public Config(IAlgorithm algorithm, Repository repository)
    {
        Algorithm = algorithm;
        Repository = repository;
        _trackedObjects = new List<IBackupObject>();
    }

    public IAlgorithm Algorithm { get; }

    public Repository Repository { get; }

    public void AddObjects(params IBackupObject[] objects)
    {
        if (objects == null)
        {
            throw new NotImplementedException();
        }

        if (objects.Any(x => x == null))
            throw new NotImplementedException();
        _trackedObjects.AddRange(objects);
    }

    public void DeleteObjects(params IBackupObject[] objects)
    {
        if (objects == null)
        {
            throw new NotImplementedException();
        }

        _trackedObjects = _trackedObjects.Except(objects).ToList();
    }
}