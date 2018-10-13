Task("Run-Tests")
    .Does<BuildData>(data => DotNetCoreTest(data.Solution.SlnPath, new DotNetCoreTestSettings()
    {
        Configuration = data.Configuration,
        NoBuild = true,
        NoRestore = true,
    }));
