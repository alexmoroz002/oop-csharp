using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;
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

    [JsonConverter(typeof(UPathConverter))]
    public UPath ArchivePath { get; }
    public IEnumerable<IBackupObject> BackupObjects { get; }
}