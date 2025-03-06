// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

namespace RepositoriesManager.configurator;

public static class SchemaCreator
{
    public static void CreateConfigurationJsonSchema(string targetFile)
    {
        File.Create(targetFile).Close();
        File.WriteAllText(
            targetFile,
            """
            {
              "$id": "",
              "$schema": "https://json-schema.org/draft/2020-12/schema",
              "title": "Configuration JSON Schema for Heber's Repositories Manager",
              "type": "object",
              "properties": {
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
