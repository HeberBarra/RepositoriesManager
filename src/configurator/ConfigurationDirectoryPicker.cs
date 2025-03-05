// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

namespace RepositoriesManager.configurator;

public static class ConfigurationDirectoryPicker
{
    private static readonly string HomeDirectory = Environment.GetFolderPath(
        Environment.SpecialFolder.UserProfile
    );
    private const string ProgramName = "RepositoriesManager";

    public static string PickConfigurationDirectory()
    {
        PlatformID osName = Environment.OSVersion.Platform;

        string? xdgConfigHomeValue = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");
        if (xdgConfigHomeValue != null)
        {
            return xdgConfigHomeValue;
        }

        switch (osName)
        {
            case PlatformID.Unix:
                return $"{HomeDirectory}/.config/{ProgramName}";
            case PlatformID.Win32S:
            case PlatformID.Win32Windows:
            case PlatformID.WinCE:
            case PlatformID.Win32NT:
                return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{ProgramName}";
            case PlatformID.MacOSX:
                return $"{HomeDirectory}/Library/Preferences/{ProgramName}";
            case PlatformID.Xbox:
            case PlatformID.Other:
            default:
                break;
        }

        return $"~/.config/{ProgramName}";
    }
}
