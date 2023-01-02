using Backups.Interfaces;
using Zio;

namespace Backups.Entities;

public class Storage : IStorage
{
    public Storage(UPath archivePath, params IBackupObject[] backupObject)
    {
        if (archivePath == null)
            throw new ArgumentNullException(nameof(archivePath));
        ArchivePath = archivePath;
        BackupObjects = backupObject;
    }

    public UPath ArchivePath { get; }
    public IEnumerable<IBackupObject> BackupObjects { get; }
}