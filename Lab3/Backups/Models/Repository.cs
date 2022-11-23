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
    public UPath Path { get; }

    public abstract Storage ArchiveObjects(UPath backupsPath, int version, params IBackupObject[] backupObjects);

    public void CreateDirectory(UPath path)
    {
        FileSystem.CreateDirectory(path);
    }

    public void CreateFile(UPath path)
    {
        FileSystem.CreateFile(path).Dispose();
    }
}