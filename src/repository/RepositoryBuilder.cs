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
    private List<Repository> Repositories { get; } = repositories;
    private string RepositoriesDirectory { get; } = repositoriesDirectory;

    private const string BuildFileBash = ".hfb_repo_manager.sh";
    private const string BuildFilePwsh = ".hfb_repo_manager.ps1";
    private readonly ProcessStartInfo _startInfoBuildBash = new()
    {
        FileName = "bash",
        Arguments = BuildFileBash,
    };

    private readonly ProcessStartInfo _startInfoBuildPwsh = new()
    {
        FileName = "powershell.exe",
        Arguments = BuildFilePwsh,
    };

    public void Build(Repository repository)
    {
        Directory.SetCurrentDirectory($"{RepositoriesDirectory}/{repository.CanonicalName}");
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
        Process buildProcess = new();

        if (files.Contains($"{Directory.GetCurrentDirectory()}/{BuildFileBash}"))
        {
            buildProcess.StartInfo = _startInfoBuildBash;
        }
        else if (files.Contains($"{Directory.GetCurrentDirectory()}/{BuildFilePwsh}"))
        {
            buildProcess.StartInfo = _startInfoBuildPwsh;
        }
        else
        {
            return;
        }

        try
        {
            buildProcess.Start();
        }
        catch (System.ComponentModel.Win32Exception e)
        {
            Console.WriteLine(
                $"Couldn't execute build file. Either permission was denied or there's something wrong with the script. Error: {e.Message}"
            );
        }
    }

    public void BuildAllRepositories()
    {
        foreach (Repository repository in Repositories)
        {
            Build(repository);
        }
    }
}
