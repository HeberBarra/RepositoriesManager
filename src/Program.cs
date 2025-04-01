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

ManagerRepositories managerRepositories = new(
    configurator.ListRepositories(),
    RepositoriesDirectoryPicker.PickRepositoriesDirectory(),
    configurator.ConfigurationInformation.TargetInstallDirectory
);

foreach (string commandLineArg in Environment.GetCommandLineArgs())
{
    if (commandLineArg == Environment.GetCommandLineArgs()[0])
        continue;

    switch (commandLineArg)
    {
        case "--clone":
            managerRepositories.CloneRepositories();
            break;

        case "--buildAll":
            managerRepositories.BuildAllRepositories();
            break;

        case "--install":
            managerRepositories.InstallRepositoriesTargets();
            break;

        default:
            Console.WriteLine($"{commandLineArg} is invalid.");
            break;
    }
}
