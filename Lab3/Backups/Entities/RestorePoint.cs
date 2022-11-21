using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private List<BackupObject> _objects;
    private DateTime _creationTime;

    public RestorePoint(List<BackupObject> objects)
    {
        _objects = objects;
        _creationTime = DateTime.Now;
    }

    public void SetTIme(DateTime time)
    {
        _creationTime = time;
    }
}