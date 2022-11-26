using Backups.Interfaces;

namespace Backups.Entities;

public class RestorePoint : IRestorePoint
{
    private List<IBackupObject> _backupObjects;
    private List<Storage> _storages;

    public RestorePoint(List<IBackupObject> objects, List<Storage> storages)
    {
        _backupObjects = objects ?? throw new ArgumentNullException(nameof(objects));
        CreationTime = DateTime.Now;
        _storages = storages ?? throw new ArgumentNullException(nameof(storages));
    }

    public IReadOnlyList<Storage> Storages => _storages;
    public IReadOnlyList<IBackupObject> BackupObjects => _backupObjects;
    public DateTime CreationTime { get; private set; }

    public void SetTIme(DateTime time)
    {
        CreationTime = time;
    }
}