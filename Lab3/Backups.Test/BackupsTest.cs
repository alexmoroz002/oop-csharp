using Backups.Entities;
using Backups.Exceptions;
using Backups.Implementations;
using Backups.Models;
using Xunit;
using Zio;

namespace Backups.Test;

public class BackupsTest
{
    [Fact]
    public void CreateDirectoryWithFileInMemoryRepository_RepositoryHasFile()
    {
        var repo = new InMemoryRepository();
        repo.CreateDirectory(UPath.Root / "Test");
        repo.CreateFile(UPath.Root / "Test" / "1.zip");
        Assert.True(repo.FileSystem.FileExists(@"/Test/1.zip"));
    }

    [Fact]
    public void CreateBackupObjects_ObjectsCreatedSuccessfully()
    {
        Repository repo = CreateInMemoryDirectoryStructure(new InMemoryRepository());
        new FolderObject(repo, @"/a/b/c/d");
        new FileObject(repo, @"\b\j\1.txt");
    }

    [Fact]
    public void CreateInvalidBackupObjects_ThrowException()
    {
        Repository repo = CreateInMemoryDirectoryStructure(new InMemoryRepository());
        Assert.Throws<FileObjectException>(() => new FileObject(repo, @"\b\j"));
        Assert.Throws<FolderObjectException>(() => new FolderObject(repo, @"\b\j\1.txt"));
    }

    [Fact]
    public void ArchiveObjectsInMemory_ArchiveIsCreated()
    {
        Repository repo = CreateInMemoryDirectoryStructure(new InMemoryRepository());
        var folderObject1 = new FolderObject(repo, @"\a\e");
        var folderObject2 = new FolderObject(repo, @"\b\j");
        var fileObject1 = new FileObject(repo, @"\a\b\c\d\1.txt");
        var fileObject2 = new FileObject(repo, @"\a\e\2.txt");
        Storage storage = repo.ArchiveObjects(@"\BackupTask 1\", 1, folderObject1, folderObject2, fileObject1, fileObject2);
        Assert.True(repo.FileSystem.FileExists(storage.ArchivePath));
    }

    [Fact]
    public void ArchiveObjectsUsingSingleStorage_ArchivesAreCreated()
    {
        Repository repo = CreateInMemoryDirectoryStructure(new InMemoryRepository());
        var config = new Config(new SingleStorageAlgorithm(), repo, UPath.Root / @"Test");
        var folderObject1 = new FolderObject(repo, @"\a\e");
        var folderObject2 = new FolderObject(repo, @"\b\j");
        var fileObject1 = new FileObject(repo, @"\a\b\c\d\1.txt");
        var fileObject2 = new FileObject(repo, @"\a\e\2.txt");

        var backupTask = new BackupTask(config);
        backupTask.CheckObjectsToBackup(folderObject1, fileObject2, folderObject2, fileObject1);
        backupTask.CreateBackup();
        Assert.True(repo.FileSystem.FileExists(@"/Test/Restore Point 1/Storage.zip"));
    }

    [Fact]
    public void ArchiveObjectsUsingSplitStorage_ThreeStoragesCreated()
    {
        Repository repo = CreateInMemoryDirectoryStructure(new InMemoryRepository());
        var config = new Config(new SplitStorageAlgorithm(), repo, UPath.Root / @"Test");
        var fileObject1 = new FileObject(repo, @"\a\b\c\d\1.txt");
        var fileObject2 = new FileObject(repo, @"\a\e\2.txt");
        var backupTask = new BackupTask(config);
        backupTask.CheckObjectsToBackup(fileObject2, fileObject1);
        backupTask.CreateBackup();
        Assert.True(config.Repository.FileSystem.FileExists(@"/Test/Restore Point 1/1.txt.zip")
                    && config.Repository.FileSystem.FileExists(@"/Test/Restore Point 1/2.txt.zip"));
        backupTask.UncheckObjectsToBackup(fileObject1);
        backupTask.CreateBackup();
        Assert.True(config.Repository.FileSystem.FileExists(@"/Test/Restore Point 2/2.txt.zip"));
        Assert.False(config.Repository.FileSystem.FileExists(@"/Test/Restore Point 2/1.txt.zip"));
    }

    private InMemoryRepository CreateInMemoryDirectoryStructure(InMemoryRepository repository)
    {
        repository.CreateDirectory(@"\BackupTask 1\");
        repository.CreateDirectory(@"\a\b\c\d");
        repository.CreateDirectory(@"\a\e\");
        repository.CreateDirectory(@"\a\b\c\d\d");
        repository.CreateDirectory(@"\b\j");

        repository.CreateFile(@"\a\b\c\d\1.txt");
        repository.CreateFile(@"\a\e\2.txt");
        repository.CreateFile(@"\b\j\1.txt");
        return repository;
    }

    private PhysicalRepository CreateOnDiskDirectoryStructure(PhysicalRepository repository)
    {
        repository.CreateDirectory(@"\mnt\c\Test\BackupTask 1\");
        repository.CreateDirectory(@"\mnt\c\Test\c\d");
        repository.CreateDirectory(@"\mnt\c\Test\e\");
        repository.CreateDirectory(@"\mnt\c\Test\b\c\d\d");
        repository.CreateDirectory(@"\mnt\c\Test\b\j");

        repository.CreateFile(@"\mnt\c\Test\c\d\1.txt");
        repository.CreateFile(@"\mnt\c\Test\e\2.txt");
        repository.CreateFile(@"\mnt\c\Test\b\j\1.txt");
        return repository;
    }
}