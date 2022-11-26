using Zio;

namespace Backups.Interfaces;

public interface IStorage
{
    UPath ArchivePath { get; }
}