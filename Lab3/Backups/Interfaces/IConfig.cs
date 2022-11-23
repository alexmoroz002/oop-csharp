using Backups.Models;
using Zio;

namespace Backups.Interfaces;

public interface IConfig
{
    IAlgorithm Algorithm { get; }
    Repository Repository { get; }
    UPath BackupPath { get; }
    IReadOnlyList<IBackupObject> BackupObjects { get; }
    void AddObjects(params IBackupObject[] objects);
    void RemoveObjects(params IBackupObject[] objects);
}