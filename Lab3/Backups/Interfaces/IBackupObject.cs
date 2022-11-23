using Zio;

namespace Backups.Interfaces;

public interface IBackupObject
{
    public UPath Path { get; }
    public string Name { get; }
    public Stream Archive(Stream source);
}