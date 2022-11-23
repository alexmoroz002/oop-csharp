using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Implementations;

public class SplitStorageAlgorithm : IAlgorithm
{
    public IEnumerable<Storage> ArchiveObject(Repository repository, UPath backupsPath, int version, IEnumerable<IBackupObject> objects)
    {
        return objects.Select(backupObject => repository.ArchiveObjects(backupsPath, version, backupObject));
    }
}