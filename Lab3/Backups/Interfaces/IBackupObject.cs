using Backups.Models;
using Zio;

namespace Backups.Interfaces;

public interface IBackupObject
{
    public UPath Path { get; }
    public string Name { get; }
    public Repository Repository { get; }
    public Stream Archive(Stream source);
}