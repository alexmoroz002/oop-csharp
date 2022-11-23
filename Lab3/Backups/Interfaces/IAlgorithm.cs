using Backups.Entities;
using Backups.Models;
using Zio;

namespace Backups.Interfaces;

public interface IAlgorithm
{
    List<Storage> ArchiveObject(Repository repository, UPath backupsPath, int version, List<IBackupObject> objects);
}