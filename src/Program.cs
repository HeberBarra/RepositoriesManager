using RepositoriesManager.configurator;
using RepositoriesManager.configurator.information;

Configurator configurator = new();
configurator.ReadConfiguration();

foreach (Repository repository in configurator.ListRepositories())
{
    Console.WriteLine(repository);
}
