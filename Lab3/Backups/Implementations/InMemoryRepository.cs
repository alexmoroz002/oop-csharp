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
        if (backupsPath == null)
            throw new ArgumentNullException(nameof(backupsPath));
        if (backupObjects == null)
            throw new ArgumentNullException(nameof(backupObjects));
        if (backupObjects.Length > 1)
            archivePath = backupsPath / @$"Restore Point {version}/Storage.zip";
        else
            archivePath = backupsPath / @$"Restore Point {version}/{backupObjects[0].Name}.zip";
        using (var stream = new MemoryStream())
        {
            foreach (IBackupObject backupObject in backupObjects)
            {
                if (backupObject == null)
                    throw new ArgumentNullException(nameof(backupObject));
                backupObject.Archive(stream);
            }

            if (!Directory.Exists(archivePath.FullName))
                CreateDirectory(archivePath.GetDirectory());
            using (Stream file = FileSystem.OpenFile(archivePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(file);
            }
        }

        return new Storage(archivePath, backupObjects);
    }
}