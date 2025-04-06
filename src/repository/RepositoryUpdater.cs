// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.
using System.Diagnostics;

namespace RepositoriesManager.repository;

public class RepositoryUpdater(List<Repository> repositories, string repositoriesDirectory)
    : IRepositoryUpdater
{
    private List<Repository> Repositories { get; } = repositories;
    private string RepositoriesDirectory { get; } = repositoriesDirectory;

    private readonly ProcessStartInfo _startInfoUpdateToLatest = new()
    {
        FileName = "git",
        Arguments = "pull",
    };

    public void Update(Repository repository)
    {
        if (!repository.Update)
            return;

        string repositoryName =
            repository.Name != string.Empty ? repository.Name : repository.Url.Segments[^1];

        try
        {
            Directory.SetCurrentDirectory($"{RepositoriesDirectory}/{repositoryName}");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("");
        }

        Process updateProcess = new() { StartInfo = _startInfoUpdateToLatest };
        updateProcess.Start();

        if (repository.CommitHash == "origin")
            return;

        ProcessStartInfo startInfoRepositorySetCommit = new()
        {
            FileName = "git",
            Arguments = $"checkout {repository.CommitHash}",
        };
        Process setCommitProcess = new() { StartInfo = startInfoRepositorySetCommit };
        setCommitProcess.Start();
    }

    public void UpdateAllRepositories()
    {
        foreach (Repository repository in Repositories)
        {
            Update(repository);
        }
    }
}
