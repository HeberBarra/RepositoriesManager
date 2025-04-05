// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using System.Text.Json;

namespace RepositoriesManager.configurator;

public class ConfiguratorReader(string configurationDirectory) : IConfigurationReader
{
    private string ConfigurationDirectory { get; set; } = configurationDirectory;
    private FileSystemWatcher? _fileSystemWatcher;

    public ConfigurationInformation Read()
    {
        string configurationFileContent = File.ReadAllText(ConfigurationDirectory + "/config.json");
        ConfigurationInformation? configurationInformation =
            JsonSerializer.Deserialize<ConfigurationInformation>(configurationFileContent);

        if (configurationInformation == null)
            throw new FileLoadException(
                "Couldn't read configuration file",
                ConfigurationDirectory + "config.json"
            );

        return configurationInformation;
    }

    public void StartWatching(string directory, FileSystemEventHandler callback)
    {
        _fileSystemWatcher = new FileSystemWatcher(directory);
        _fileSystemWatcher.NotifyFilter = NotifyFilters.FileName;
        _fileSystemWatcher.Changed += callback;
    }
}
