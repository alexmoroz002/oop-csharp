using Backups.Extra.Entities;
using Backups.Interfaces;
using Newtonsoft.Json;
using Serilog;

namespace Backups.Extra.Models.Implementations;

public class HybridLimitAlgorithm : ILimitAlgorithm
{
    [JsonProperty("AlgorithmList")]
    private IEnumerable<ILimitAlgorithm> _algorithms;

    [JsonProperty("All")]
    private bool _all;

    public HybridLimitAlgorithm(IEnumerable<ILimitAlgorithm> algorithms, bool all)
    {
        Log.Information("Creating new HybridLimitAlgorithm with algorithms: {0}, parameter: {1}", (object)algorithms, all);
        ArgumentNullException.ThrowIfNull(algorithms);
        ArgumentNullException.ThrowIfNull(all);

        _algorithms = algorithms;
        _all = all;
        Log.Information("Algorithm created");
    }

    public IEnumerable<IRestorePoint> Execute(IEnumerable<IRestorePoint> points)
    {
        Log.Information("Applying HybridLimitAlgorithm with algorithms: {0}, parameter: {1}", (object)_algorithms, _all);
        ArgumentNullException.ThrowIfNull(points);

        var restorePoints = new List<IRestorePoint>();
        var pointsList = points.ToList();
        foreach (ILimitAlgorithm limitAlgorithm in _algorithms)
        {
            restorePoints.AddRange(limitAlgorithm.Execute(pointsList));
        }

        IEnumerable<IRestorePoint> result = _all
            ? restorePoints.GroupBy(x => x).Where(x => x.Count() == _algorithms.Count()).Select(x => x.First()).ToList()
            : restorePoints.Distinct();

        Log.Information("Algorithm executed successfully");
        return result;
    }
}