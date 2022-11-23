namespace Backups.Interfaces;

public interface IConfig
{
    public void AddObjects(params IBackupObject[] objects);
    public void RemoveObjects(params IBackupObject[] objects);
}