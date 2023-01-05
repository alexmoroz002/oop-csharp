using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Extra.Entities;

public interface IBackupTaskExtra : IBackupTask
{
    public ILimitAlgorithm LimitAlgorithm { get; set; }
    public bool MergePoints { get; set; }
    public void RestoreFromPoint(IRestorePoint rp);
    public void RestoreFromPointTo(IRestorePoint rp, Repository destRepo, UPath destFolder);
    public void ClearPoints();
}