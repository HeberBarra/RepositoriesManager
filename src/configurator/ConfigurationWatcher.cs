namespace RepositoriesManager.configurator;

public static class ConfigurationWatcher
{
    private static FileSystemWatcher? _fileSystemWatcher;

    public static void StartWatchingFile(
        string configurationDirectory,
        FileSystemEventHandler callbackMethod
    )
    {
        _fileSystemWatcher = new FileSystemWatcher(configurationDirectory);
        _fileSystemWatcher.NotifyFilter = NotifyFilters.FileName;
        _fileSystemWatcher.Changed += callbackMethod;
    }
}
