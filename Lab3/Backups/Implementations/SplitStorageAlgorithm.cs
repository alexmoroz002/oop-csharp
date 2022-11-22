using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio.FileSystems;

namespace Backups.Implementations;

public class SplitStorageAlgorithm : IAlgorithm
{
    public List<Storage> ArchiveObject(Repository repository, List<IBackupObject> objects)
    {
        return objects.Select(backupObject => repository.ArchiveObjects(backupObject)).ToList();
    }
}