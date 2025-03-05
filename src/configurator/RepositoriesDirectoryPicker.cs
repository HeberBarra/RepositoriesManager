namespace RepositoriesManager.configurator;

public static class RepositoriesDirectoryPicker
{
    private static readonly string HomeDirectory = Environment.GetFolderPath(
        Environment.SpecialFolder.UserProfile
    );
    private const string ProgramName = "RepositoriesManager";

    public static string PickRepositoriesDirectory()
    {
        PlatformID osName = Environment.OSVersion.Platform;
        string? xdgDataHome = Environment.GetEnvironmentVariable("XDG_DATA_HOME");

        if (xdgDataHome != null)
        {
            return xdgDataHome;
        }

        switch (osName)
        {
            case PlatformID.Unix:
                return $"{HomeDirectory}/.local/share/{ProgramName}/repos";
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
            case PlatformID.Win32NT:
                return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{ProgramName}/repos";
            case PlatformID.MacOSX:
                return $"{HomeDirectory}/Library/{ProgramName}/repos";
            case PlatformID.Xbox:
            case PlatformID.Other:
            default:
                break;
        }

        return $"~/.local/share/{ProgramName}/repos";
    }
}
