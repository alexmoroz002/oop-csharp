using Backups.Entities;
using Backups.Implementations;
using Backups.Interfaces;
using Backups.Models;
using Serilog;
using Zio;

namespace Backups.Extra.Entities;

public class SplitStorageAlgorithmLogging : IAlgorithm
{
    private readonly IAlgorithm _algorithm = new SplitStorageAlgorithm();
    public List<Storage> ArchiveObject(Repository repository, UPath backupsPath, int version, IEnumerable<IBackupObject> objects)
    {
        Log.Information("Archiving {0} in {1} to {2} using {3}", string.Join(',', objects.Select(x => x.Name)), repository, backupsPath, this);
        List<Storage> result = _algorithm.ArchiveObject(repository, backupsPath, version, objects);
        Log.Information("Archiving completed");
        return result;
    }
}