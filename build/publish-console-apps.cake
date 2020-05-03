Task("Publish-Console-Apps")
    .DoesForEach<BuildData, string>(
        data => data.Solution.ProjectsPath,
        (data, projectPath, _) => DotNetCorePublish(projectPath, new DotNetCorePublishSettings
        {
            Configuration = data.Configuration,
            NoBuild = true,
            NoRestore = true,
            OutputDirectory = data.Solution.ArtifactsDirectory,
            MSBuildSettings = new DotNetCoreMSBuildSettings
            {
                NoLogo = true,
            },
        }));
