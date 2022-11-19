using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private IEnumerable<BackupObject> objects;
    private DateTime creationTime;
}