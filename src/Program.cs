﻿// Copyright (C) 2025 Heber Ferreira Barra.
// Licensed under the Massachusetts Institute of Technology (MIT) License.
// You may obtain a copy of the license at:
// https://choosealicense.com/licenses/mit/
// A short and simple permissive license with conditions only requiring preservation of copyright and license notices.
// Licensed works, modifications, and larger works may be distributed under different terms and without source code.

using RepositoriesManager.configurator;
using RepositoriesManager.configurator.information;

Configurator configurator = new();
configurator.ReadConfiguration();
configurator.WatchConfigurationFile();

foreach (Repository repository in configurator.ListRepositories())
{
    Console.WriteLine(repository);
}
