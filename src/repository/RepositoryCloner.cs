// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using System.Diagnostics;

namespace RepositoriesManager.repository;

public class RepositoryCloner(List<Repository> repositories, string repositoriesDirectory)
    : IRepositoryCloner
{
    private List<Repository> Repositories { get; } = repositories;
    private string RepositoriesDirectory { get; } = repositoriesDirectory;
    private readonly ProcessStartInfo _cloneProcessStartInfo = new() { FileName = "git" };

    public void Clone(Repository repository)
    {
        _cloneProcessStartInfo.Arguments = repository.RecurseSubmodules
            ? $"clone --recurse-submodules {repository.Url} {RepositoriesDirectory}/{repository.CanonicalName}"
            : $"clone {repository.Url} {RepositoriesDirectory}/{repository.CanonicalName}";
        Process process = new() { StartInfo = _cloneProcessStartInfo };
        process.Start();
    }

    public void CloneAllRepositories()
    {
        foreach (Repository repository in Repositories)
        {
            Clone(repository);
        }
    }
}
