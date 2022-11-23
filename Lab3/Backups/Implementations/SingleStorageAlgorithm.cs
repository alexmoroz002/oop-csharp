using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Implementations;

public class SingleStorageAlgorithm : IAlgorithm
{
    public IEnumerable<Storage> ArchiveObject(Repository repository, UPath backupsPath, int version, IEnumerable<IBackupObject> objects)
    {
        yield return repository.ArchiveObjects(backupsPath, version, objects.ToArray());
    }
}