namespace RepositoriesManager.configurator.information;

public record Repository(
    string Name,
    bool Update,
    string CommitHash,
    string ExecutableFile,
    Uri Url
);
