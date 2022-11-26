using Backups.Entities;

namespace Backups.Interfaces;

public interface IRestorePoint
{
    IReadOnlyList<Storage> Storages { get; }
    IReadOnlyList<IBackupObject> BackupObjects { get; }
    DateTime CreationTime { get; }

    void SetTIme(DateTime time);
}