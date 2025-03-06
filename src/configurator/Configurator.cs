// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using System.Text.Json;
using System.Text.Json.Nodes;
using RepositoriesManager.configurator.information;

namespace RepositoriesManager.configurator;

public class Configurator
{
    private static readonly string ConfigurationDirectory =
        ConfigurationDirectoryPicker.PickConfigurationDirectory();

    private readonly string _configurationFile = $"{ConfigurationDirectory}/config.json";

    private ConfigurationInformation _configurationInformation = new();

    public Configurator()
    {
        CreateDirectories();
        CreateConfigurationFile();
    }

    private static void CreateDirectories()
    {
        string repositoriesDirectory = RepositoriesDirectoryPicker.PickRepositoriesDirectory();

        Directory.CreateDirectory(ConfigurationDirectory);
        Directory.CreateDirectory(repositoriesDirectory);
    }

    private void CreateConfigurationFile()
    {
        SchemaCreator.CreateConfigurationJsonSchema($"{ConfigurationDirectory}/config.schema.json");

        if (File.Exists(_configurationFile))
        {
            return;
        }

        File.Create(_configurationFile).Close();
        string baseConfiguration =
            JsonNode.Parse(JsonSerializer.Serialize(_configurationInformation))?.ToString()
            ?? string.Empty;
        File.WriteAllText(_configurationFile, baseConfiguration);
    }

    public void ReadConfiguration()
    {
        string configurationFileContent = File.ReadAllText(_configurationFile);
        ConfigurationInformation? configuration =
            JsonSerializer.Deserialize<ConfigurationInformation>(configurationFileContent);

        if (configuration != null)
        {
            _configurationInformation = configuration;
        }

        Console.WriteLine(
            "Configuration couldn't be loaded, please verify your configuration. Terminating program..."
        );
        Environment.Exit(1);
    }

    public List<Repository> ListRepositories()
    {
        return _configurationInformation.Repositories;
    }
}
