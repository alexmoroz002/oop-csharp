using Backups.Entities;
using Backups.Extra.Decorators;
using Backups.Extra.Decorators.Intetrfaces;
using Backups.Extra.Entities;
using Backups.Extra.Models.Implementations;
using Backups.Extra.Services;
using Backups.Implementations;
using Backups.Interfaces;
using Backups.Models;
using Xunit;
using Zio;

namespace Backups.Extra.Test;

public class BackupTestExtra
{
    public static IEnumerable<object[]> AlgorithmType =>
        new List<object[]>()
        {
            new object[] { new SingleStorageAlgorithmLogging() },
            new object[] { new SplitStorageAlgorithmLogging() },
        };

    [Theory]
    [MemberData(nameof(AlgorithmType))]
    public void CreateRestorePoint_RestoreToSameLocation_DataIsRestored(IAlgorithm algorithm)
    {
        IBackupServiceExtra service = new BackupServiceExtra(false);
        IConfig config = new Config(algorithm, new InMemoryRepository(), @"/mnt/c/Test");

        CreateDirectoryStructure(config.Repository);
        int itemsBeforeRestore = config.Repository.FileSystem.EnumerateItems(@"/mnt/c/Test/a", SearchOption.AllDirectories).Count() +
                                 config.Repository.FileSystem.EnumerateItems(@"/mnt/c/Test/b", SearchOption.AllDirectories).Count();
        config.AddObjects(SelectBackupObjects(config));
        IBackupTaskExtra task = service.CreateTask(config);
        IRestorePoint rp = service.RunTask(task);
        RemoveDirectoryStructure(config.Repository);

        service.RestoreBackup(task, rp);
        int itemsAfterRestore = config.Repository.FileSystem.EnumerateItems(@"/mnt/c/Test/a", SearchOption.AllDirectories).Count() +
                                config.Repository.FileSystem.EnumerateItems(@"/mnt/c/Test/b", SearchOption.AllDirectories).Count();

        Assert.Equal(itemsBeforeRestore, itemsAfterRestore);
    }

    [Theory]
    [MemberData(nameof(AlgorithmType))]
    public void CreateRestorePoint_RestoreToDifferentLocation_DataIsRestored(IAlgorithm algorithm)
    {
        IBackupServiceExtra service = new BackupServiceExtra(false);
        IConfig config = new Config(algorithm, new InMemoryRepository(), @"/mnt/c/Test");

        CreateDirectoryStructure(config.Repository);
        config.AddObjects(SelectBackupObjects(config));
        IBackupTaskExtra task = service.CreateTask(config);
        IRestorePoint rp = service.RunTask(task);
        RemoveDirectoryStructure(config.Repository);

        service.RestoreBackupTo(task, rp, config.Repository, "/mnt/c/Test/newLocation");
        IEnumerable<FileSystemItem> itemsAfterRestore = config.Repository.FileSystem.EnumerateItems("/mnt/c/Test/newLocation", SearchOption.TopDirectoryOnly);

        Assert.Equal(rp.BackupObjects.Count, itemsAfterRestore.Count());
    }

    [Theory]
    [MemberData(nameof(AlgorithmType))]
    public void CreateRestorePoints_ApplyCountLimit_PointsCountChanged(IAlgorithm algorithm)
    {
        IBackupServiceExtra service = new BackupServiceExtra(false);
        IConfig config = new Config(algorithm, new InMemoryRepository(), @"/mnt/c/Test");

        CreateDirectoryStructure(config.Repository);
        config.AddObjects(SelectBackupObjects(config));
        IBackupTaskExtra task = service.CreateTask(config);
        service.RunTask(task);
        IBackupObject file1 = new FileObject(config.Repository, @"\mnt\c\Test\b\j\3.txt");
        IBackupObject file2 = new FileObject(config.Repository, @"\mnt\c\Test\b\j\4.txt");
        config.AddObjects(file1, file2);
        service.RunTask(task);
        config.RemoveObjects(file1, file2);
        service.RunTask(task);
        service.RunTask(task);
        service.RunTask(task);

        task.LimitAlgorithm = new CountLimitAlgorithm(3);
        task.MergePoints = true;
        service.PurgeRestorePoints(task);

        Assert.Equal(3, task.Backup.RestorePoints.Count);
    }

    [Theory]
    [MemberData(nameof(AlgorithmType))]
    public void CreateRestorePoints_ApplyTimeLimit_PointsCountChanged(IAlgorithm algorithm)
    {
        IBackupServiceExtra service = new BackupServiceExtra(false);
        IConfig config = new Config(algorithm, new InMemoryRepository(), @"/mnt/c/Test");

        CreateDirectoryStructure(config.Repository);
        config.AddObjects(SelectBackupObjects(config));
        IBackupTaskExtra task = service.CreateTask(config);
        service.RunTask(task);
        IBackupObject file1 = new FileObject(config.Repository, @"\mnt\c\Test\b\j\3.txt");
        IBackupObject file2 = new FileObject(config.Repository, @"\mnt\c\Test\b\j\4.txt");
        config.AddObjects(file1, file2);
        service.RunTask(task);
        config.RemoveObjects(file1, file2);
        service.RunTask(task);

        DateTime limitDateTime = DateTime.Now;
        task.LimitAlgorithm = new TimeLimitAlgorithm(limitDateTime);
        task.MergePoints = false;
        service.RunTask(task);
        service.RunTask(task);
        service.PurgeRestorePoints(task);

        Assert.All(task.Backup.RestorePoints, rp => Assert.True(rp.CreationTime > limitDateTime));
    }

    [Theory]
    [MemberData(nameof(AlgorithmType))]
    public void CreateRestorePoints_ApplyHybridLimit_PointsCountChanged(IAlgorithm algorithm)
    {
        IBackupServiceExtra service = new BackupServiceExtra(false);
        IConfig config = new Config(algorithm, new InMemoryRepository(), @"/mnt/c/Test");

        CreateDirectoryStructure(config.Repository);
        config.AddObjects(SelectBackupObjects(config));
        IBackupTaskExtra task = service.CreateTask(config);
        service.RunTask(task);
        IBackupObject file1 = new FileObject(config.Repository, @"\mnt\c\Test\b\j\3.txt");
        IBackupObject file2 = new FileObject(config.Repository, @"\mnt\c\Test\b\j\4.txt");
        config.AddObjects(file1, file2);
        service.RunTask(task);
        config.RemoveObjects(file1, file2);
        service.RunTask(task);
        service.RunTask(task);

        IEnumerable<ILimitAlgorithm> algorithms = new List<ILimitAlgorithm>() { new TimeLimitAlgorithm(new DateTime(2020, 3, 9, 20, 0, 0)), new CountLimitAlgorithm(2) };
        task.LimitAlgorithm = new HybridLimitAlgorithm(algorithms, true);
        task.MergePoints = true;
        service.PurgeRestorePoints(task);

        Assert.True(task.Backup.RestorePoints.Count > 2);
    }

    private Repository CreateDirectoryStructure(Repository repository)
    {
        if (repository.FileSystem.DirectoryExists(@"\mnt\c\Test"))
            repository.FileSystem.DeleteDirectory(@"\mnt\c\Test", true);
        repository.CreateDirectory(@"\mnt\c\Test\a\b\c\d");
        repository.CreateDirectory(@"\mnt\c\Test\a\e\");
        repository.CreateDirectory(@"\mnt\c\Test\b\c\d\d");
        repository.CreateDirectory(@"\mnt\c\Test\b\j");
        repository.CreateFile(@"\mnt\c\Test\b\j\3.txt");
        repository.CreateFile(@"\mnt\c\Test\b\j\4.txt");
        repository.CreateFile(@"\mnt\c\Test\a\b\c\d\1.txt");
        repository.CreateFile(@"\mnt\c\Test\a\e\2.txt");
        repository.CreateFile(@"\mnt\c\Test\b\j\1.txt");
        return repository;
    }

    private Repository RemoveDirectoryStructure(Repository repository)
    {
        repository.FileSystem.DeleteDirectory(@"\mnt\c\Test\a", true);
        repository.FileSystem.DeleteDirectory(@"\mnt\c\Test\b", true);
        return repository;
    }

    private IBackupObject[] SelectBackupObjects(IConfig config)
    {
        return new IBackupObject[]
        {
            new FolderObject(config.Repository, @"/mnt/c/Test/a\e"),
            new FolderObject(config.Repository, @"/mnt/c/Test\b\j"),
            new FolderObject(config.Repository, @"\mnt\c\Test\b\c\d\d"),
            new FileObject(config.Repository, @"/mnt/c/Test\a\b\c\d\1.txt"),
            new FileObject(config.Repository, @"/mnt/c/Test\a\e\2.txt"),
        };
    }
}