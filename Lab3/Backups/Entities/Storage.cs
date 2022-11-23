using Backups.Interfaces;
using Zio;

namespace Backups.Entities;

public class Storage : IStorage
{
    private UPath _archivePath;

    public Storage(UPath archivePath)
    {
        _archivePath = archivePath;
    }
}