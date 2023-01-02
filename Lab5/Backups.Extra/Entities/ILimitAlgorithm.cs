using Backups.Interfaces;

namespace Backups.Extra.Entities;

public interface ILimitAlgorithm
{
    public IEnumerable<IRestorePoint> Execute(IEnumerable<IRestorePoint> points);
}