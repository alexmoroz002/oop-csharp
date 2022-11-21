using Zio;
using Zio.FileSystems;

namespace Backups.Models;

public abstract class Repository
{
    protected Repository(FileSystem fileSystem)
    {
        FileSystem = fileSystem;
    }

    protected FileSystem FileSystem { get; }

    protected void CreateDirectory(UPath path)
    {
        FileSystem.CreateDirectory(path);
    }
}