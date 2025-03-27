// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using System.Diagnostics;
using RepositoriesManager.configurator.information;

namespace RepositoriesManager.repository;

public class RepositoryCloner(List<Repository> repositories, string targetDirectory)
{
    private List<Repository> Repositories { get; } = repositories;
    private string TargetDirectory { get; } = targetDirectory;

    public void CloneRepositories()
    {
        foreach (Repository repository in Repositories)
        {
            ProcessStartInfo processStartInfo = new() { FileName = "git" };
            string repositoryName =
                repository.Name != string.Empty ? repository.Name : repository.Url.Segments[^1];

            processStartInfo.Arguments =
                $"clone {repository.Url} {TargetDirectory}/{repositoryName}";

            Process process = new();
            process.StartInfo = processStartInfo;
            process.Start();
        }
    }
}
