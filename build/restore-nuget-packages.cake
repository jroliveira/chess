Task("Restore-NuGet-Packages")
    .Does<BuildData>(data => DotNetCoreRestore(data.Solution.SlnPath));
