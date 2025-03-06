// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

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
