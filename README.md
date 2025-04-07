<h1 style="text-align: center" >Repositories Manager</h1>

![GitHub License](https://img.shields.io/github/license/HeberBarra/RepositoriesManager?logo=GitHub&label=License)
![Project Top Language](https://img.shields.io/github/languages/top/HeberBarra/RepositoriesManager?label=CSharp)

A Simple C# for managing git repositories with ease.

## Configuration

On the first run the program will create a configuration file with a JSON schema file, in one of the following directories,
depending on your operational system(OS):

- Windows: %AppData%/RepositoriesManager/
- Linux: ~/.config/RepositoriesManager/
- MacOS: ~/Library/Preferences/RepositoriesManager/

If the environment variable XDG_CONFIG_HOME the configuration directory will be created there instead.

### Configuration values

- Repositories Directory: Where the repositories will be cloned into.
- Target Installation Directory: Where the repositories binaries will be linked to if specified.

Repositories, each one has the following properties:

- URL: The URL of the remote repository;
- Name(Optional): The display name of the repository;
- RecurseSubmodules: Whether the recurse submodules option should be used when cloning the repository;
- Update: Whether the repository should be updated;
- CommitHash: If the repository update property is set to true will ensure that the repository stays in that commit;
- ExecutableFile: The executable file that will be linked over to installation directory.

## Command Line Arguments

In order to execute one of the program operations it's necessary to pass one of following command line arguments:

- `--clone-all`: Clones all configured repositories;
- `--build-all`: Builds all configured repositories;
- `--install-all`: Installs the specified file of each one of the configured repositories;
- `--remove-unknown`: Deletes all the repositories that are not present in the configuration. __Warning:__ Won't ask for confirmation;
- `--remove-all`: Deletes all the repositories present in the repositories directory;
- `--update-all`: Updates all configured repositories, except those that the update property set to `false`.

## License

This project is under MIT - Massachusetts Institute of Technology. A short and simple permissive license with conditions
only requiring preservation of copyright and license notices. Licensed works, modifications, and larger works may be
distributed under different terms and without source code.
