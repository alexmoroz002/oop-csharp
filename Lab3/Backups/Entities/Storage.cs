using Backups.Interfaces;
using Zio;

namespace Backups.Entities;

public class Storage : IStorage
{
    public Storage(UPath archivePath)
    {
        ArchivePath = archivePath;
    }

    public UPath ArchivePath { get; }
}