using System.Diagnostics;
using RepositoriesManager.configurator.information;

namespace RepositoriesManager.repository;

public class RepositoryBuilder(List<Repository> repositories, string reposDirectory)
{
    private const string BuildFileBash = ".hfb_repo_manager.sh";
    private const string BuildFilePwsh = ".hfb_repo_manager.ps1";
    private List<Repository> Repositories { get; } = repositories;
    private string ReposDirectory { get; } = reposDirectory;

    public void BuildRepositories()
    {
        ProcessStartInfo startInfoBuildBash = new()
        {
            FileName = "bash",
            Arguments = BuildFileBash,
        };
        ProcessStartInfo startInfoBuildPwsh = new()
        {
            FileName = "powershell.exe",
            Arguments = BuildFilePwsh,
        };

        foreach (
            string repositoryName in Repositories.Select(repository =>
                repository.Name != string.Empty ? repository.Name : repository.Url.Segments[^1]
            )
        )
        {
            Directory.SetCurrentDirectory($"{ReposDirectory}/{repositoryName}");

            string[] currentDirectoryFiles = Directory.GetFiles(Directory.GetCurrentDirectory());

            Process processBuild = new();
            if (
                currentDirectoryFiles.Contains($"{Directory.GetCurrentDirectory()}/{BuildFileBash}")
            )
            {
                processBuild.StartInfo = startInfoBuildBash;
            }
            else if (
                currentDirectoryFiles.Contains($"{Directory.GetCurrentDirectory()}/{BuildFilePwsh}")
            )
            {
                processBuild.StartInfo = startInfoBuildPwsh;
            }
            else
            {
                return;
            }

            try
            {
                processBuild.Start();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                Console.WriteLine(
                    "Couldn't execute build file. Either permission was denied or there's something wrong with the script."
                );
            }
        }
    }
}
