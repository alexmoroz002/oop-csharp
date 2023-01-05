using Backups.Interfaces;
using Backups.Models;
using Zio;

namespace Backups.Extra.Entities;

public interface IBackupServiceExtra
{
    public IBackupTaskExtra CreateTask(IConfig config);
    public IRestorePoint RunTask(IBackupTask task);
    public void RestoreBackup(IBackupTaskExtra task, IRestorePoint restorePoint);
    public void RestoreBackupTo(IBackupTaskExtra task, IRestorePoint restorePoint, Repository destRepo, UPath destFolder);
    public void AddObjectsToTask(IBackupTask task, params IBackupObject[] objects);
    public void RemoveObjectsFromTask(IBackupTask task, params IBackupObject[] objects);
    public void LogToConsole(bool addTimeStamp);
    public void LogToFile(string path, bool addTimeStamp);
    public void PurgeRestorePoints(IBackupTaskExtra task);
}