// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using RepositoriesManager.configurator.information;

namespace RepositoriesManager.repository;

public class ManagerRepositories(
    List<Repository> repositories,
    string repositoriesDirectory,
    string installationDirectory
)
{
    private readonly RepositoryBuilder _repositoryBuilder = new(
        repositories,
        repositoriesDirectory
    );
    private readonly RepositoryCloner _repositoryCloner = new(repositories, repositoriesDirectory);

    private readonly RepositoryInstaller _repositoryInstaller = new(
        repositories,
        repositoriesDirectory,
        installationDirectory
    );

    public void CloneRepositories()
    {
        _repositoryCloner.CloneRepositories();
    }

    public void BuildAllRepositories()
    {
        _repositoryBuilder.BuildRepositories();
    }

    public void InstallRepositoriesTargets()
    {
        _repositoryInstaller.InstallTargets();
    }
}
