// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

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
