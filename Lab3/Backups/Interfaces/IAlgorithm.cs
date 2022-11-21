using Backups.Entities;

namespace Backups.Interfaces;

public interface IAlgorithm
{
    List<Storage> ArchiveObject();
}