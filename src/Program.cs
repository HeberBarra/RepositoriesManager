// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using RepositoriesManager.configurator;
using RepositoriesManager.repository;

namespace RepositoriesManager;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        IConfigurationDirectoryPicker configurationDirectoryPicker =
            new ConfigurationDirectoryPicker();
        IConfigurator configurator = new Configurator(configurationDirectoryPicker);
        configurator.CreateConfiguration();
        if (args.Length == 0)
        {
            Console.WriteLine(
                "Please pass as argument one of the following options: --clone-all, --build-all, --install-all, --remove-all, --remove-unknown, --update-all"
            );
            return;
        }

        Console.WriteLine(
            "To avoid problems, please assert that your global .gitignore is properly configured to ignore .hfb_repo_manager.ps1 or .hfb_repo_manager.sh"
        );

        configurator.ReadConfiguration();
        configurator.StartWatchingConfiguration();

        RepositoryManager repositoryManager = new(
            configurator.GetRepositories(),
            configurator.GetRepositoriesDirectory(),
            configurator.GetTargetInstallDirectory()
        );

        foreach (string arg in args)
        {
            switch (arg)
            {
                case "--clone-all":
                    repositoryManager.CloneAllRepositories();
                    break;

                case "--build-all":
                    repositoryManager.BuildAllRepositories();
                    break;

                case "--install-all":
                    repositoryManager.InstallAllRepositories();
                    break;

                case "--remove-all":
                    repositoryManager.RemoveAllRepositories();
                    break;

                case "--remove-unknown":
                    repositoryManager.RemoveUnknownRepositories();
                    break;

                case "--update-all":
                    repositoryManager.UpdateAllRepositories();
                    break;
            }
        }
    }
}
