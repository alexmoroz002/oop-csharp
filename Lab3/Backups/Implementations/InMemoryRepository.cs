using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Implementations;

public class InMemoryRepository : Repository
{
    public InMemoryRepository()
        : base(new MemoryFileSystem()) { }

    public override Storage ArchiveObjects(UPath backupsPath, int version, params IBackupObject[] backupObjects)
    {
        UPath archivePath;
        if (backupObjects.Length > 1)
            archivePath = backupsPath / @$"Restore Point {version} + /Single Storage.zip";
        else
            archivePath = backupsPath / @$"Restore Point {version} + /{backupObjects[0].GetName()}.zip";
        using (var stream = new MemoryStream())
        {
            foreach (IBackupObject backupObject in backupObjects)
            {
                backupObject.Archive(stream);
            }

            using (Stream file = FileSystem.OpenFile(archivePath, FileMode.CreateNew, FileAccess.Write))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(file);
            }
        }

        return new Storage(archivePath);
    }
}