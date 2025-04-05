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
        throw new NotImplementedException();
    }

    public void RemoveAllRepositories()
    {
        throw new NotImplementedException();
    }

    public void RemoveUnknownRepositories()
    {
        throw new NotImplementedException();
    }
}
