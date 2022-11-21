using Backups.Models;
using Zio.FileSystems;

namespace Backups.Entities;

public class PhysicalRepository : Repository
{
    public PhysicalRepository()
        : base(new PhysicalFileSystem()) { }
}