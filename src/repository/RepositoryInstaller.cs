// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using RepositoriesManager.configurator.information;

namespace RepositoriesManager.repository;

public class RepositoryInstaller(
    List<Repository> repositories,
    string repositoriesDirectory,
    string installationDirectory
)
{
    private readonly List<Repository> _repositories = repositories;
    private readonly string _repositoriesDirectory = repositoriesDirectory;
    private readonly string _installationDirectory = installationDirectory;

    public void InstallTargets()
    {
        foreach (Repository repository in _repositories)
        {
            if (
                repository.ExecutableFile == string.Empty
                || !File.Exists(
                    $"{_repositoriesDirectory}/{repository.Name}/{repository.ExecutableFile}"
                )
            )
                continue;

            Directory.CreateDirectory(_installationDirectory);
            try
            {
                File.CreateSymbolicLink(
                    $"{_installationDirectory}/{repository.ExecutableFile}",
                    $"{_repositoriesDirectory}/{repository.Name}/{repository.ExecutableFile}"
                );
            }
            catch (IOException)
            {
                Console.WriteLine(
                    "Couldn't create symbolic link. Either file exists or something else went wrong."
                );
            }
        }
    }
}
