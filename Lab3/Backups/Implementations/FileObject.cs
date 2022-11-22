using Backups.Interfaces;
using Zio;

namespace Backups.Implementations;

public class FileObject : IBackupObject
{
    public UPath Path { get; }
}