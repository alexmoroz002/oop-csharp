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

    public UPath Path { get; protected init; }
    public FileSystem FileSystem { get; }

    public abstract Storage ArchiveObjects(UPath backupsPath, int version, params IBackupObject[] backupObjects);

    public void CreateDirectory(UPath path)
    {
        FileSystem.CreateDirectory(path);
    }

    public void CreateFile(UPath path)
    {
        FileSystem.CreateFile(path).Dispose();
    }

    public FileAttributes GetObjectAttributes(UPath path)
    {
        return FileSystem.GetAttributes(path);
    }

    public void CopyFile(UPath sourcePath, IFileSystem destFileSystem, UPath destPath, bool overwrite)
    {
        FileSystem.CopyFileCross(sourcePath, destFileSystem, destPath, overwrite);
    }

    public void CopyDirectory(UPath sourcePath, IFileSystem destFileSystem, UPath destPath, bool overwrite)
    {
        FileSystem.CopyDirectory(sourcePath, destFileSystem, destPath, overwrite);
    }

    public bool FileExists(UPath path)
    {
        return FileSystem.FileExists(path);
    }
}