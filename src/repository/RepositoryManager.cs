// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

namespace RepositoriesManager.repository;

public class RepositoryManager
{
    private IRepositoryBuilder RepositoryBuilder { get; }
    private IRepositoryCloner RepositoryCloner { get; }
    private IRepositoryInstaller RepositoryInstaller { get; }
    private IRepositoryRemover RepositoryRemover { get; }
    private IRepositoryUpdater RepositoryUpdater { get; }

    public RepositoryManager(
        IRepositoryBuilder repositoryBuilder,
        IRepositoryCloner repositoryCloner,
        IRepositoryInstaller repositoryInstaller,
        IRepositoryRemover repositoryRemover,
        IRepositoryUpdater repositoryUpdater
    )
    {
        RepositoryBuilder = repositoryBuilder;
        RepositoryCloner = repositoryCloner;
        RepositoryInstaller = repositoryInstaller;
        RepositoryRemover = repositoryRemover;
        RepositoryUpdater = repositoryUpdater;
    }

    public RepositoryManager(
        List<Repository> repositories,
        string repositoriesDirectory,
        string targetInstallationDirectory
    )
    {
        RepositoryBuilder = new RepositoryBuilder(repositories, repositoriesDirectory);
        RepositoryCloner = new RepositoryCloner(repositories, repositoriesDirectory);
        RepositoryInstaller = new RepositoryInstaller(
            repositories,
            repositoriesDirectory,
            targetInstallationDirectory
        );
        RepositoryRemover = new RepositoryRemover(
            repositories,
            repositoriesDirectory,
            targetInstallationDirectory
        );
        RepositoryUpdater = new RepositoryUpdater(repositories, repositoriesDirectory);
    }

    public void BuildRepository(Repository repository)
    {
        RepositoryBuilder.Build(repository);
    }

    public void BuildAllRepositories()
    {
        RepositoryBuilder.BuildAllRepositories();
    }

    public void CloneRepository(Repository repository)
    {
        RepositoryCloner.Clone(repository);
    }

    public void CloneAllRepositories()
    {
        RepositoryCloner.CloneAllRepositories();
    }

    public void InstallRepository(Repository repository)
    {
        RepositoryInstaller.Install(repository);
    }

    public void InstallAllRepositories()
    {
        RepositoryInstaller.InstallAllRepositories();
    }

    public void RemoveRepository(Repository repository)
    {
        RepositoryRemover.Remove(repository);
    }

    public void RemoveUnknownRepositories()
    {
        RepositoryRemover.RemoveUnknownRepositories();
    }

    public void RemoveAllRepositories()
    {
        RepositoryRemover.RemoveAllRepositories();
    }

    public void UpdateRepository(Repository repository)
    {
        RepositoryUpdater.Update(repository);
    }

    public void UpdateAllRepositories()
    {
        RepositoryUpdater.UpdateAllRepositories();
    }
}
