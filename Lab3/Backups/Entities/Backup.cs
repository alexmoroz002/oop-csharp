using Backups.Interfaces;

namespace Backups.Entities;

public class Backup : IBackup
{
    private List<RestorePoint> _restorePoints;

    public RestorePoint CreateRestorePoint(Config config)
    {
        config.Repository.
    }
}