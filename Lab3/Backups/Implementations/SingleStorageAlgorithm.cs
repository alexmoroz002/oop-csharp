using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Implementations;

public class SingleStorageAlgorithm : IAlgorithm
{
    public List<Storage> ArchiveObject(Repository repository, List<IBackupObject> objects)
    {
        var archive = new List<Storage>();
        archive.Add(repository.ArchiveObjects(objects.ToArray()));
        return archive;
    }
}