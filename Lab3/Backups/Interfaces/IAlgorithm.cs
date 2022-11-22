using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IAlgorithm
{
    List<Storage> ArchiveObject(Repository repository, List<IBackupObject> objects);
}