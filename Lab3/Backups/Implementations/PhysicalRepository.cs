using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Implementations;

public class PhysicalRepository : Repository
{
    public PhysicalRepository()
        : base(new PhysicalFileSystem())
    {
        Path = UPath.Root;
    }

    public override Storage ArchiveObjects(UPath backupsPath, int version, params IBackupObject[] backupObjects)
    {
        UPath archivePath;
        if (backupObjects.Length > 1)
            archivePath = backupsPath / @$"Restore Point {version}/Storage.zip";
        else
            archivePath = backupsPath / @$"Restore Point {version}/{backupObjects[0].Name}.zip";
        using (var stream = new MemoryStream())
        {
            foreach (IBackupObject backupObject in backupObjects)
            {
                backupObject.Archive(stream);
            }

            if (!Directory.Exists(archivePath.GetDirectory().FullName))
                CreateDirectory(archivePath.GetDirectory());
            using (Stream file = FileSystem.OpenFile(archivePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(file);
            }
        }

        return new Storage(archivePath);
    }
}