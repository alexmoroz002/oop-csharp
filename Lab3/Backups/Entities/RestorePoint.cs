using Backups.Interfaces;
using Newtonsoft.Json;

namespace Backups.Entities;

public class RestorePoint : IRestorePoint
{
    [JsonProperty("BackupObjectList")]
    private List<IBackupObject> _backupObjects;

    [JsonProperty("StorageList")]
    private List<Storage> _storages;

    public RestorePoint(List<IBackupObject> objects, List<Storage> storages)
    {
        _backupObjects = objects ?? throw new ArgumentNullException(nameof(objects));
        CreationTime = DateTime.Now;
        _storages = storages ?? throw new ArgumentNullException(nameof(storages));
    }

    [JsonIgnore]
    public IReadOnlyList<Storage> Storages => _storages;

    [JsonIgnore]
    public IReadOnlyList<IBackupObject> BackupObjects => _backupObjects;
    public DateTime CreationTime { get; private set; }

    public void SetTIme(DateTime time)
    {
        CreationTime = time;
    }
}