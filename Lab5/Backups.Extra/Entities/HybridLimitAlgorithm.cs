using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class HybridLimitAlgorithm : ILimitAlgorithm
{
    private IEnumerable<ILimitAlgorithm> _algorithms;
    private bool _all;

    public HybridLimitAlgorithm(IEnumerable<ILimitAlgorithm> algorithms, bool all)
    {
        _algorithms = algorithms;
        _all = all;
    }

    public IEnumerable<IRestorePoint> Execute(IEnumerable<IRestorePoint> points)
    {
        var restorePoints = new List<IRestorePoint>();
        var pointsList = points.ToList();
        foreach (ILimitAlgorithm limitAlgorithm in _algorithms)
        {
            restorePoints.AddRange(limitAlgorithm.Execute(pointsList));
        }

        if (_all)
            return restorePoints.GroupBy(x => x).Where(x => x.Count() == _algorithms.Count()).Select(x => x.First()).ToList();

        return restorePoints.Distinct();
    }
}