// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using System.Diagnostics;

namespace RepositoriesManager.repository;

public class RepositoryBuilder(List<Repository> repositories, string repositoriesDirectory)
    : IRepositoryBuilder
{
    private const string BuildFileBash = ".hfb_repo_manager.sh";
    private const string BuildFilePwsh = ".hfb_repo_manager.ps1";
    private List<Repository> Repositories { get; } = repositories;
    private string RepositoriesDirectory { get; } = repositoriesDirectory;

    public void Build(Repository repository)
    {
        throw new NotImplementedException();
    }

    public void BuildAllRepositories()
    {
        ProcessStartInfo startInfoBuildBash = new()
        {
            FileName = "bash",
            Arguments = BuildFileBash,
        };
        ProcessStartInfo startInfoBuildPwsh = new()
        {
            FileName = "powershell.exe",
            Arguments = BuildFilePwsh,
        };

        foreach (
            string repositoryName in Repositories.Select(repository =>
                repository.Name != string.Empty ? repository.Name : repository.Url.Segments[^1]
            )
        )
        {
            Directory.SetCurrentDirectory($"{RepositoriesDirectory}/{repositoryName}");

            string[] currentDirectoryFiles = Directory.GetFiles(Directory.GetCurrentDirectory());

            Process processBuild = new();
            if (
                currentDirectoryFiles.Contains($"{Directory.GetCurrentDirectory()}/{BuildFileBash}")
            )
            {
                processBuild.StartInfo = startInfoBuildBash;
            }
            else if (
                currentDirectoryFiles.Contains($"{Directory.GetCurrentDirectory()}/{BuildFilePwsh}")
            )
            {
                processBuild.StartInfo = startInfoBuildPwsh;
            }
            else
            {
                return;
            }

            try
            {
                processBuild.Start();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                Console.WriteLine(
                    "Couldn't execute build file. Either permission was denied or there's something wrong with the script."
                );
            }
        }
    }
}
