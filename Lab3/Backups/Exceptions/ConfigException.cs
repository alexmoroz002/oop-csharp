using Zio;

namespace Backups.Exceptions;

public class ConfigException : Exception
{
    private ConfigException(string e)
        : base(e) { }

    public static ConfigException NullException()
    {
        return new ConfigException("Object is null");
    }

    public static ConfigException ObjectNameException()
    {
        return new ConfigException("Unable to archive multiple objects with same name");
    }

    public static ConfigException PathIsRelativeException(UPath path)
    {
        return new ConfigException($"{path} is not absolute");
    }
}