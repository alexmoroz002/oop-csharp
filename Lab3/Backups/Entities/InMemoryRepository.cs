using Backups.Models;
using Zio.FileSystems;

namespace Backups.Entities;

public class InMemoryRepository : Repository
{
    public InMemoryRepository()
        : base(new MemoryFileSystem()) { }
}