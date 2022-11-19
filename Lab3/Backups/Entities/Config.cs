using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class Config
{
    private IAlgorithm algorithm;
    private Repository repository;
}