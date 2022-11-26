using Backups.Interfaces;
using Zio;

namespace Backups.Entities;

public class Storage : IStorage
{
    public Storage(UPath archivePath)
    {
        if (archivePath == null)
            throw new ArgumentNullException(nameof(archivePath));
        ArchivePath = archivePath;
    }

    public UPath ArchivePath { get; }
}