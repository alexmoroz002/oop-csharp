using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackup
{
    public RestorePoint CreateRestorePoint(IConfig config);
}