// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace NuGet.PackageManagement.UI.Options
{
    public class ConfigPathsViewModel
    {
        public string ConfigPath { get; set; }
        public ConfigPathsViewModel(string path)
        {
            ConfigPath = path ?? throw new ArgumentNullException(nameof(path));
        }

        public override string ToString()
        {
            return ConfigPath;
        }
    }
}
