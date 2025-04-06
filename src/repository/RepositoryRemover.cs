// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.
namespace RepositoriesManager.repository;

public class RepositoryRemover(
    List<Repository> repositories,
    string repositoriesDirectory,
    string targetInstallationDirectory
) : IRepositoryRemover
{
    private List<Repository> Repositories { get; } = repositories;
    private string RepositoriesDirectory { get; } = repositoriesDirectory;
    private string TargetInstallationDirectory { get; } = targetInstallationDirectory;

    public void Remove(Repository repository)
    {
        string repositoryName =
            repository.Name != string.Empty ? repository.Name : repository.Url.Segments[^1];
        try
        {
            File.Delete($"{TargetInstallationDirectory}/{repository.ExecutableFile}");
            Directory.Delete(
                $"{RepositoriesDirectory}/{repositoryName}/{repository.ExecutableFile}",
                true
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"Couldn't the following repository: {repositoryName}. Error: {e.Message}"
            );
        }
    }

    public void RemoveAllRepositories()
    {
        foreach (Repository repository in Repositories)
        {
            Remove(repository);
        }
    }

    private string GetRepositoryDirectory(Repository repository)
    {
        string repositoryName =
            repository.Name != string.Empty ? repository.Name : repository.Url.Segments[^1];

        if (RepositoriesDirectory.EndsWith('/'))
        {
            return $"{RepositoriesDirectory}{repositoryName}";
        }
        else
        {
            return $"{RepositoriesDirectory}/{repositoryName}";
        }
    }

    public void RemoveUnknownRepositories()
    {
        string[] repositoriesDirectories = Directory.GetDirectories(RepositoriesDirectory);
        List<string> repositoriesNames = [];
        repositoriesNames.AddRange(Repositories.Select(GetRepositoryDirectory));

        foreach (string repositoryDirectory in repositoriesDirectories)
        {
            if (repositoriesNames.Contains(repositoryDirectory))
                continue;

            try
            {
                Directory.Delete(repositoryDirectory, true);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(
                    $"Couldn't delete the repository: {repositoryDirectory}. Error: {e.Message}"
                );
            }
        }
    }
}
