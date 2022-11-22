using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio.FileSystems;

namespace Backups.Implementations;

public class InMemoryRepository : Repository
{
    public InMemoryRepository()
        : base(new MemoryFileSystem()) { }

    public override Storage ArchiveObjects(params IBackupObject[] backupObjects)
    {
        /*
        using (var memoryStream = new MemoryStream())
        {
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (IBackupObject backupObject in backupObjects)
                {
                    archive.CreateEntryFromFile(backupObject.Path,);
                    var x = new ZipArchiveFileSystem();
                    x.
                }
            }

            using (var fileStream = new FileStream(@"C:\Temp\test.zip", FileMode.Create))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(fileStream);
            }
        }
        */
    }
}