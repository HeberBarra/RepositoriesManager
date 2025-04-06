// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

namespace RepositoriesManager.repository;

public class RepositoryInstaller(
    List<Repository> repositories,
    string repositoriesDirectory,
    string targetInstallationDirectory
) : IRepositoryInstaller
{
    private List<Repository> Repositories { get; } = repositories;
    private string RepositoriesDirectory { get; } = repositoriesDirectory;
    private string TargetInstallationDirectory { get; } = targetInstallationDirectory;

    public void Install(Repository repository)
    {
        string repositoryName =
            repository.Name != string.Empty ? repository.Name : repository.Url.Segments[^1];
        if (
            repository.ExecutableFile == string.Empty
            || !File.Exists($"{RepositoriesDirectory}/{repositoryName}/{repository.ExecutableFile}")
        )
            return;

        if (!Directory.Exists(TargetInstallationDirectory))
        {
            Directory.CreateDirectory(TargetInstallationDirectory);
        }

        try
        {
            File.CreateSymbolicLink(
                $"{TargetInstallationDirectory}/{repository.ExecutableFile}",
                $"{RepositoriesDirectory}/{repositoryName}/{repository.ExecutableFile}"
            );
        }
        catch (IOException e)
        {
            Console.WriteLine(
                $"Couldn't create symbolic link. Either file exists or something else went wrong. Error: {e.Message}"
            );
        }
    }

    public void InstallAllRepositories()
    {
        foreach (Repository repository in Repositories)
        {
            Install(repository);
        }
    }
}
