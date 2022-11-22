using Backups.Interfaces;
using Zio;

namespace Backups.Implementations;

public class FolderObject : IBackupObject
{
    public UPath Path { get; }
}