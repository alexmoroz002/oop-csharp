using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Implementations;

public class SplitStorageAlgorithm : IAlgorithm
{
    public List<Storage> ArchiveObject(Repository repository, UPath backupsPath, int version, IEnumerable<IBackupObject> objects)
    {
        if (repository == null)
            throw new ArgumentNullException(nameof(repository));
        if (backupsPath == null)
            throw new ArgumentNullException(nameof(backupsPath));
        if (objects == null || objects.Any(x => x == null))
            throw new ArgumentNullException(nameof(objects));
        return objects.Select(backupObject => repository.ArchiveObjects(backupsPath, version, backupObject)).ToList();
    }
}