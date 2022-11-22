using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio.FileSystems;

namespace Backups.Implementations;

public class PhysicalRepository : Repository
{
    public PhysicalRepository()
        : base(new PhysicalFileSystem()) { }

    public override Storage ArchiveObjects(params IBackupObject[] backupObjects)
    {
        throw new NotImplementedException();
    }
}