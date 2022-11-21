using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class Config
{
    private IAlgorithm _algorithm;
    private Repository _repository;
    private List<BackupObject> _trackedObjects;

    public Config(IAlgorithm algorithm, Repository repository)
    {
        _algorithm = algorithm;
        _repository = repository;
        _trackedObjects = new List<BackupObject>();
    }

    public void AddObjects(params BackupObject[] objects)
    {
        if (objects == null)
        {
            throw new NotImplementedException();
        }

        if (objects.Any(x => x == null))
            throw new NotImplementedException();
        _trackedObjects.AddRange(objects);
    }

    public void DeleteObjects(params BackupObject[] objects)
    {
        if (objects == null)
        {
            throw new NotImplementedException();
        }

        _trackedObjects = _trackedObjects.Except(objects).ToList();
    }
}