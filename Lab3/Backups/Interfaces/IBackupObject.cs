namespace Backups.Interfaces;

public interface IBackupObject
{
    public Stream Archive(Stream source);
    public string GetName();
}