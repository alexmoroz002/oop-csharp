using Backups.Interfaces;

namespace Backups.Entities;

public class RestorePoint : IRestorePoint
{
    private List<IBackupObject> _objects;
    private List<Storage> _backupedObjects;
    private DateTime _creationTime;

    public RestorePoint(List<IBackupObject> objects)
    {
        _objects = objects;
        _creationTime = DateTime.Now;
        _backupedObjects = new List<Storage>();
    }

    public void SetTIme(DateTime time)
    {
        _creationTime = time;
    }
}