Task("Build-Solution")
    .Does<BuildData>(data => DotNetCoreBuild(data.Solution.SlnPath, new DotNetCoreBuildSettings()
    {
        Configuration = data.Configuration,
        NoRestore = true,
        MSBuildSettings = new DotNetCoreMSBuildSettings
        {
            NoLogo = true,
        },
    }));
