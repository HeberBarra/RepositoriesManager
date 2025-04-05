// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.
namespace RepositoriesManager.repository;

public class RepositoryUpdater(List<Repository> repositories, string repositoriesDirectory)
    : IRepositoryUpdater
{
    private List<Repository> Repositories { get; } = repositories;
    private string RepositoriesDirectory { get; } = repositoriesDirectory;

    public void Update(Repository repository)
    {
        throw new NotImplementedException();
    }

    public void UpdateAllRepositories()
    {
        throw new NotImplementedException();
    }
}
