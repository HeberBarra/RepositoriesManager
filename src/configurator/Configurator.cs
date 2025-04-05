// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using RepositoriesManager.repository;

namespace RepositoriesManager.configurator;

public class Configurator : IConfigurator
{
    private ConfigurationInformation? _configurationInformation;
    private readonly IConfigurationCreator _configurationCreator;
    private readonly IConfigurationDirectoryPicker _configurationDirectoryPicker;
    private readonly IConfigurationReader _configurationReader;
    private readonly IConfigurationVerifier _configurationVerifier;

    public Configurator(
        ConfigurationInformation configurationInformation,
        IConfigurationCreator configurationCreator,
        IConfigurationDirectoryPicker configurationDirectoryPicker,
        IConfigurationReader configurationReader,
        IConfigurationVerifier configurationVerifier
    )
    {
        _configurationInformation = configurationInformation;
        _configurationCreator = configurationCreator;
        _configurationDirectoryPicker = configurationDirectoryPicker;
        _configurationReader = configurationReader;
        _configurationVerifier = configurationVerifier;
    }

    public Configurator(IConfigurationDirectoryPicker configurationDirectoryPicker)
    {
        string configurationDirectory = configurationDirectoryPicker.PickConfigurationDirectory();
        _configurationDirectoryPicker = configurationDirectoryPicker;
        _configurationCreator = new ConfigurationCreator(configurationDirectory);
        _configurationReader = new ConfiguratorReader(configurationDirectory);
        _configurationVerifier = new ConfigurationVerifier();
    }

    public List<Repository> GetRepositories()
    {
        return _configurationInformation?.Repositories ?? [];
    }

    public string GetRepositoriesDirectory()
    {
        return _configurationInformation?.RepositoriesDirectory ?? string.Empty;
    }

    public string GetTargetInstallDirectory()
    {
        return _configurationInformation?.TargetInstallDirectory ?? string.Empty;
    }

    public void CreateConfiguration()
    {
        _configurationCreator.CreateConfigurationSchemaFile();
        _configurationCreator.CreateConfigurationFile();
    }

    public void ReadConfiguration()
    {
        _configurationInformation = _configurationReader.Read();
    }

    private void ReloadConfiguration(object obj, FileSystemEventArgs args)
    {
        ReadConfiguration();
    }

    public void StartWatchingConfiguration()
    {
        _configurationReader.StartWatching(
            _configurationDirectoryPicker.PickConfigurationDirectory(),
            ReloadConfiguration
        );
    }

    public bool VerifyConfiguration()
    {
        return _configurationInformation != null
            && _configurationVerifier.Verify(_configurationInformation);
    }
}
