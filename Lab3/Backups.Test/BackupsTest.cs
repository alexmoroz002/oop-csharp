using Backups.Implementations;
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
}