using Backups.Entities;
using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Models;

public abstract class Repository
{
    protected Repository(FileSystem fileSystem)
    {
        FileSystem = fileSystem;
    }

    public FileSystem FileSystem { get; }

    public abstract Storage ArchiveObjects(params IBackupObject[] backupObjects);

    protected void CreateDirectory(UPath path)
    {
        FileSystem.CreateDirectory(path);
    }
}