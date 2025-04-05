// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using System.Text.Json;
using System.Text.Json.Nodes;

namespace RepositoriesManager.configurator;

public class ConfigurationCreator(string configurationDirectory) : IConfigurationCreator
{
    private ConfigurationInformation? ConfigurationInformation { get; set; }

    private static void CreateConfigurationDirectory(string directory)
    {
        Directory.CreateDirectory(directory);
    }

    public void CreateConfigurationFile()
    {
        CreateConfigurationDirectory(configurationDirectory);

        string configurationFile = configurationDirectory + "/config.json";
        if (File.Exists(configurationFile))
            return;

        string baseConfiguration =
            JsonNode.Parse(JsonSerializer.Serialize(ConfigurationInformation))?.ToString()
            ?? string.Empty;

        File.Create(configurationFile).Close();
        File.WriteAllText(configurationFile, baseConfiguration);
    }

    public void CreateConfigurationSchemaFile()
    {
        CreateConfigurationDirectory(configurationDirectory);
        string schemaFile = configurationDirectory + "/config.schema.json";
        File.Create(schemaFile).Close();
        File.WriteAllText(
            schemaFile,
            """
            {
              "$id": "",
              "$schema": "https://json-schema.org/draft/2020-12/schema",
              "title": "Configuration JSON Schema for Heber's Repositories Manager",
              "type": "object",
              "properties": {
                "RepositoriesDirectory" : {
                  "type": "string",
                  "description": "The directory to which the repositories will be cloned."
                },
                "TargetInstallDirectory": {
                  "type": "string",
                  "description": "The directory to which the compiled files should be linked to."
                },
                "Repositories": {
                  "type": "array",
                  "items": {
                    "type": "object",
                    "properties": {
                      "Name": {
                        "description": "The repository's display name",
                        "type": "string"
                      },
                      "Update": {
                        "description": "Whether or not the repository should be updated",
                        "type": "boolean"
                      },
                      "CommitHash": {
                        "description": "The repository's current commit hash. Will be ignored if update is set to true",
                        "type": "string"
                      },
                      "ExecutableFile":
                      {
                        "description": "The executable file that should be linked over to the target install directory. Use blank string to disable linkage",
                        "type": "string"
                      },
                      "Url": {
                        "description": "The repository's URL that will be used to clone it with Git",
                        "type": "string"
                      }
                    },
                    "required": ["Name", "Update", "Update", "ExecutableFile", "Url"]
                  }
                }
              }
            }

            """
        );
    }
}
