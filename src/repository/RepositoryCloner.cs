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
