// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using RepositoriesManager.configurator;
using RepositoriesManager.repository;

Console.WriteLine(
    "To avoid problems, please assert that your global .gitignore is properly configured to ignore .hfb_repo_manager.ps1 or .hfb_repo_manager.sh"
);

Configurator configurator = new();
configurator.ReadConfiguration();
configurator.WatchConfigurationFile();

RepositoryCloner repositoryCloner = new(
    configurator.ListRepositories(),
    RepositoriesDirectoryPicker.PickRepositoriesDirectory()
);

RepositoryBuilder repositoryBuilder = new(
    configurator.ListRepositories(),
    RepositoriesDirectoryPicker.PickRepositoriesDirectory()
);

foreach (string commandLineArg in Environment.GetCommandLineArgs())
{
    switch (commandLineArg)
    {
        case "--clone":
            repositoryCloner.CloneRepositories();
            break;

        case "--build":
            Console.WriteLine("Here");
            repositoryBuilder.BuildRepositories();
            break;
    }
}
