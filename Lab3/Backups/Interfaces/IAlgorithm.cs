using Backups.Entities;
using Backups.Models;
using Zio;

namespace Backups.Interfaces;

public interface IAlgorithm
{
    IEnumerable<Storage> ArchiveObject(Repository repository, UPath backupsPath, int version, IEnumerable<IBackupObject> objects);
}