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

    private readonly ConfigurationInformation _configurationInformation = new();

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
}
