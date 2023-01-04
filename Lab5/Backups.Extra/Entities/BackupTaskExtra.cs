using System.Text;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;
using Zio;
using Zio.FileSystems;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : IBackupTask
{
    [JsonProperty("BackupTask")]
    private readonly IBackupTask _backupTask;

    public BackupTaskExtra(IConfig config)
    {
        Backup = new BackupExtra();
        _backupTask = new BackupTask(config);
        LimitAlgorithm = new TimeLimitAlgorithm(DateTime.Now);
        MergePoints = false;
    }

    public IConfig Config => _backupTask.Config;
    public IBackup Backup { get; }
    public ILimitAlgorithm LimitAlgorithm { get; set; }
    public bool MergePoints { get; set; }

    public RestorePoint CreateRestorePoint()
    {
        return Backup.CreateRestorePoint(_backupTask.Config);
    }

    public void RestoreFromPoint(IRestorePoint rp)
    {
        foreach (Storage storage in rp.Storages)
        {
            using var stream = new MemoryStream();
            using (Stream file =
                   Config.Repository.FileSystem.OpenFile(storage.ArchivePath, FileMode.Open, FileAccess.Read))
            {
                file.Seek(0, SeekOrigin.Begin);
                file.CopyTo(stream);
            }

            using var fs = new ZipArchiveFileSystem(stream);
            IEnumerable<FileSystemItem> files = fs.EnumerateItems(UPath.Root, SearchOption.TopDirectoryOnly);
            foreach (FileSystemItem item in files)
            {
                IBackupObject backupObject = storage.BackupObjects.First(o => o.Name == item.GetName());
                if (item.IsDirectory)
                {
                    fs.CopyDirectory(item.FullName, Config.Repository.FileSystem, backupObject.Path, true);
                }
                else
                {
                    Config.Repository.FileSystem.CreateDirectory(backupObject.Path.GetDirectory());
                    fs.CopyFileCross(item.FullName, Config.Repository.FileSystem, backupObject.Path, true);
                }
            }
        }
    }

    public void RestoreFromPointTo(IRestorePoint rp, Repository destRepo, UPath destFolder)
    {
        foreach (Storage storage in rp.Storages)
        {
            using var stream = new MemoryStream();
            using (Stream file =
                   Config.Repository.FileSystem.OpenFile(storage.ArchivePath, FileMode.Open, FileAccess.Read))
            {
                file.Seek(0, SeekOrigin.Begin);
                file.CopyTo(stream);
            }

            using var fs = new ZipArchiveFileSystem(stream);
            IEnumerable<FileSystemItem> files = fs.EnumerateItems(UPath.Root, SearchOption.TopDirectoryOnly);
            foreach (FileSystemItem item in files)
            {
                IBackupObject backupObject = storage.BackupObjects.First(o => o.Name == item.GetName());
                if (item.IsDirectory)
                {
                    fs.CopyDirectory(item.FullName, destRepo.FileSystem, destFolder / backupObject.Name, true);
                }
                else
                {
                    fs.CopyFileCross(item.FullName, destRepo.FileSystem, destFolder / backupObject.Name, true);
                }
            }
        }
    }

    public void CleanPoints()
    {
        IEnumerable<IRestorePoint> pointsToClean = LimitAlgorithm.Execute(Backup.RestorePoints).ToList();
        if (!MergePoints)
        {
            foreach (IRestorePoint restorePoint in pointsToClean)
            {
                Config.Repository.FileSystem.DeleteDirectory(restorePoint.Storages[0].ArchivePath.GetDirectory(), true);
            }

            Backup.RemovePoints(pointsToClean.ToArray());
            return;
        }

        pointsToClean = pointsToClean.Append(Backup.RestorePoints.Except(pointsToClean).First()).OrderBy(x => x.CreationTime);
        List<IBackupObject> newBackupObjects = new ();
        foreach (IRestorePoint restorePoint in pointsToClean)
        {
            if (restorePoint.Storages.Count != 1 || restorePoint.Storages[0].BackupObjects.Count() <= 1)
            {
                newBackupObjects.AddRange(restorePoint.BackupObjects);
                newBackupObjects = newBackupObjects.Distinct().ToList();
                RestoreFromPoint(restorePoint);
            }

            Config.Repository.FileSystem.DeleteDirectory(restorePoint.Storages[0].ArchivePath.GetDirectory(), true);
            Backup.RemovePoints(restorePoint);
        }

        ConfigExtra newConfig = new (new Config(new SplitStorageAlgorithmLogging(), Config.Repository, Config.BackupPath));
        newConfig.AddObjects(newBackupObjects.ToArray());
        Backup.CreateRestorePoint(newConfig);
    }

    public void CheckObjectsToBackup(params IBackupObject[] objects)
    {
        _backupTask.CheckObjectsToBackup(objects);
    }

    public void UncheckObjectsToBackup(params IBackupObject[] objects)
    {
        _backupTask.UncheckObjectsToBackup(objects);
    }
}